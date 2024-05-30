using CMS_back.Data;
using CMS_back.IGenericRepository;
using CMS_back.Interfaces;
using CMS_back.Mailing;
using CMS_back.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CMS_back.Services
{
    public class ControlTaskRepository : IControlTaskRepository
    {
        public CMSContext Context { get; }
        public IHttpContextAccessor ContextAccessor { get; }
        public UserManager<ApplicationUser> Usermanager { get; }
        public IUserRepository userRepo { get; }

        public ControlTaskRepository(CMSContext context, IHttpContextAccessor contextAccessor,
            UserManager<ApplicationUser> usermanager, IUserRepository repo )
        {
            Context=context;
            ContextAccessor=contextAccessor;
            Usermanager=usermanager;
            userRepo=repo;
        }
        public async Task<Control_Task?> Create(Control_Task task, List<string> usersTasksIds, string controlId)
        {
            task.CreationDate = DateTime.Now;
            task.CreateBy = await userRepo.GetCurrentUser();
            task.ControlID = controlId;
            task.UserTasks = new List<Control_UserTasks>();
            foreach (var userId in usersTasksIds)
            {
                Control_UserTasks control_UserTasks = new Control_UserTasks()
                {
                    Control_TaskID = task.Id,
                    UserTaskID = userId
                };
                task.UserTasks.Add(control_UserTasks);
            }
            Context.Control_Task.Add(task);
            if (await Context.SaveChangesAsync() > 0)
            {
                await Context.Entry(task)
                .Collection(t => t.UserTasks)
                .Query()
                .Include(ut => ut.UserTask)
                .LoadAsync();
                return task;
            }
            return null;
        }
        public async Task<ICollection<Control_Task>?> GetTasksOfControl(string controlId)
        {
            var currentUser = await userRepo.GetCurrentUser();
            var tasks = Context.Control_Task.Include(
                task => task.UserTasks
                ).ThenInclude(
                    task => task.UserTask
                ).Where(
                    controlTask => controlTask.ControlID == controlId
                ).ToList();
            return tasks;
        }
        public async Task<ICollection<Control_Task>?> GetUserTasks(string controlId, string userId)
        {
            var tasks = Context.Control_Task.Include(
                    controlTask => controlTask.UserTasks
                ).ThenInclude(
                    user => user.UserTask
                ).Where(
                    controlTask => controlTask.ControlID == controlId
                ).Where(
                    controlTask => controlTask.UserTasks.Any(
                            userTask => userTask.UserTask.Id == userId
                        )
                ).ToList();
            return tasks;
        }
        public async Task<Control_Task?> UpdateTask(Control_Task Task, List<string> usersTasksIds)
        {
            var task =await Context.Control_Task.Include(
                task => task.Control
                ).Include(
                    task => task.CreateBy
                ).Include(
                    tasks => tasks.UserTasks
                ).ThenInclude(
                    users => users.UserTask
                ).FirstOrDefaultAsync(
                    task => task.Id == Task.Id
                );
            if ( task == null ) { return null; }

            task.Description = Task.Description;
            task.CreationDate = DateTime.Now;
            task.CreateBy = await userRepo.GetCurrentUser();
            task.UserTasks.Clear();

            var tasksToRemove = task.UserTasks.Where(
                    ut => !usersTasksIds.Contains(ut.UserTaskID)
                ).ToList();

            foreach (var taskToRemove in tasksToRemove)
            {
                Context.Remove(taskToRemove);
            }

            foreach (var userId in usersTasksIds)
            {
                var existingUserTask = task.UserTasks.FirstOrDefault(
                    ut => ut.UserTaskID == userId);

                if (existingUserTask == null)
                {
                    // If it doesn't exist, create a new one
                    Control_UserTasks newUserTask = new Control_UserTasks()
                    {
                        Control_TaskID = task.Id,
                        UserTaskID = userId,
                    };
                    task.UserTasks.Add(newUserTask);
                }
            }
            Context.Attach(task);
            if (await Context.SaveChangesAsync() > 0){
                await Context.Entry(task)
                .Collection(t => t.UserTasks)
                .Query()
                .Include(ut => ut.UserTask)
                .LoadAsync();
                return task; 
            }
            return null;
        }
        public async Task<bool> DeleteTask(string taskId)
        {
            var task = Context.Control_Task.FirstOrDefault(task => task.Id == taskId );
            if (task == null) return false;
            Context.Control_Task.Remove(task);
            if(await Context.SaveChangesAsync() > 0) { return true; }
            return false;
        }
        public async Task<Control_Task?> FinishTask(string taskId)
        {
            var task = Context.Control_Task.Include(
                    control => control.CreateBy
                ).Include(
                    control => control.Control
                ).FirstOrDefault(
                    task => task.Id == taskId
                );
            if (task == null) return null;
            task.IsDone = Question.Yes;
            Context.Update(task);
            if(await Context.SaveChangesAsync() > 0) return task;
            return null;
        }
        public async Task<Control_Task?> GetTaskByID(string taskId)
        {
            var task = await Context.Control_Task
                .Include(controlTask => controlTask.Control)
                .Include(controlTask => controlTask.CreateBy)
                .Include(controlTask => controlTask.UserTasks)
                .ThenInclude(users => users.UserTask)
                .FirstOrDefaultAsync(t => t.Id == taskId);
            return task;
        }

    }
}
