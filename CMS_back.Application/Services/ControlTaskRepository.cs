using AutoMapper;
using CMS_back.Application.Helpers;
using CMS_back.Consts;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.IGenericRepository;
using CMS_back.Interfaces;
using CMS_back.Mailing;
using CMS_back.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CMS_back.Services
{

    #region ExpressionExtensions
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var body = Expression.AndAlso(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }

    #endregion

    public class ControlTaskRepository : IControlTaskRepository
    {
        private readonly CMSContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserRepository _userRepo;
        private readonly IGenericRepository<Control_Task> _controlTaskRepo;
        private readonly IGenericRepository<ControlUsers> _controlUserRepo;
        private readonly IGenericRepository<Control> _controlRepo;
        private readonly IMailingService _mailingService;
        private readonly IUserHelpers _userHelpers;
        private readonly IMapper _mapper;

        public ControlTaskRepository(CMSContext context, IHttpContextAccessor contextAccessor,
            UserManager<ApplicationUser> usermanager, IUserRepository repo, IGenericRepository<Control_Task> genericRepository
            , IMailingService mailingService, IGenericRepository<Control> controlRepo, IGenericRepository<ControlUsers> controlUserRepo
            , IUserHelpers userHelpers, IMapper mapper)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _usermanager = usermanager;
            _userRepo = repo;
            _controlTaskRepo = genericRepository;
            _mailingService = mailingService;
            _controlUserRepo = controlUserRepo;
            _controlRepo = controlRepo;
            _userHelpers = userHelpers;
            _mapper = mapper;
        }

        public async Task<Control_Task?> Create(Control_Task task, List<string> usersTasksIds, string controlId)
        {
            var currentUser = await _userHelpers.GetCurrentUserAsync();

            var control = await _controlRepo.GetById(controlId);
            if (control == null) throw new Exception("Control Not Found");

            var isHead = await _controlUserRepo.FindFirstAsync(controlUser => controlUser.UserID == currentUser.Id && controlUser.ControlID == control.Id);
            if (isHead == null || isHead.JobType != JobType.Head) throw new Exception("Head of Control Only Has access");

            task.CreationDate = DateTime.Now;
            task.CreateBy = currentUser;
            task.ControlID = controlId;
            task.UserTasks = new List<Control_UserTasks>();
            foreach (var userId in usersTasksIds)
            {
                var userTask = _context.ApplicationUser.FirstOrDefault(user => user.Id == userId);
                if (userTask == null)
                {
                    throw new Exception($"UserTask with ID {userId} not found");
                }
                Control_UserTasks control_UserTasks = new Control_UserTasks()
                {
                    Control_TaskID = task.Id,
                    UserTaskID = userId,
                    UserTask = userTask
                };
                task.UserTasks.Add(control_UserTasks);
            }

            foreach (var userTask in task.UserTasks)
            {

                if (userTask.UserTask.Email != null)
                {
                    var message = new MailMessage(new string[] { userTask.UserTask.Email }, "Control System", $"Control {control.Name}:\n Head of control {currentUser.Name} assign new task to you, The Task is {task.Description}.");
                    _mailingService.SendMail(message);
                }
            }
            _controlTaskRepo.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Control_Task?> UpdateTask(Control_Task Task, List<string> usersTasksIds)
        {
            var task = await _controlTaskRepo.FindFirstAsync(ts => ts.Id == Task.Id, new[] { "Control", "CreateBy", "UserTasks", "UserTasks.UserTask" });
            if (task == null) return null;

            task.Description = Task.Description;
            task.CreationDate = DateTime.Now;
            task.CreateBy = await _userHelpers.GetCurrentUserAsync();

            if (task.UserTasks == null)
            {
                await _context.Entry(task).Collection(t => t.UserTasks).LoadAsync();
            }
            task.UserTasks.Clear();

            var tasksToRemove = task.UserTasks.Where(ut => !usersTasksIds.Contains(ut.UserTaskID)).ToList();
            foreach (var taskToRemove in tasksToRemove)
            {
                _context.Remove(taskToRemove);
            }
            foreach (var userId in usersTasksIds)
            {
                var existingUserTask = task.UserTasks.FirstOrDefault(ut => ut.UserTaskID == userId);
                if (existingUserTask == null)
                {
                    Control_UserTasks newUserTask = new Control_UserTasks
                    {
                        Control_TaskID = task.Id,
                        UserTaskID = userId,
                    };
                    task.UserTasks.Add(newUserTask);
                }
            }
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<IEnumerable<ControlTaskResultDTO>?> GetTasksOfControl(string controlId)
        {
            var currentUser = await _userHelpers.GetCurrentUserAsync();
            if (currentUser == null) throw new Exception("No user Login yet");
            var tasks = await _controlTaskRepo.FindAsync(c => c.ControlID == controlId, ["UserTasks", "UserTasks.UserTask"]);
            var results = _mapper.Map<IEnumerable<ControlTaskResultDTO>>(tasks);
            return results;
        }

        public async Task<IEnumerable<ControlTaskResultDTO>?> GetUserTasks(string controlId, string userId)
        {
            Expression<Func<Control_Task, bool>> condition1 = c => c.ControlID == controlId;
            Expression<Func<Control_Task, bool>> condition2 = c => c.UserTasks.Any(ut => ut.UserTaskID == userId);
            var combinedCondition = condition1.AndAlso(condition2);

            var tasks = await _controlTaskRepo.FindAsync(combinedCondition, ["UserTasks", "UserTasks.UserTask"]);
            var results = _mapper.Map<IEnumerable<ControlTaskResultDTO>>(tasks);
            return results;
        }

        public async Task<bool> DeleteTask(string taskId)
        {

            var task = await _controlTaskRepo.FindFirstAsync(task => task.Id == taskId);
            if (task == null) throw new Exception("Not Found Task");

            var currentUser = await _userHelpers.GetCurrentUserAsync();
            var controlTask = await _controlTaskRepo.FindFirstAsync(ts => ts.Id == taskId);
            var isHead = await _controlUserRepo.FindFirstAsync(user => user.UserID == currentUser.Id && user.ControlID == controlTask.ControlID);
            if (isHead == null || isHead.JobType != JobType.Head) throw new Exception("Head of control only has access");

            _controlTaskRepo.Remove(task);
            if (await _context.SaveChangesAsync() > 0) return true;
            return false;

        }

        public async Task<Control_Task?> GetTaskByID(string taskId)
        {
            //var task = await _context.Control_Task
            //    .Include(controlTask => controlTask.Control)
            //    .Include(controlTask => controlTask.CreateBy)
            //    .Include(controlTask => controlTask.UserTasks)
            //    .ThenInclude(users => users.UserTask)
            //    .FirstOrDefaultAsync(t => t.Id == taskId);
            var task = await _controlTaskRepo.FindFirstAsync(ts => ts.Id == taskId, ["Control", "CreateBy", "UserTasks", "UserTasks.UserTask"]);
            return task;
        }

        public async Task<bool> FinishTask(string taskId)
        {
            var task = await _controlTaskRepo.FindFirstAsync(ts => ts.Id == taskId, ["Control", "CreateBy"]);
            if (task == null) throw new Exception("This Task Is Not Found");
            task.IsDone = Question.Yes;
            _controlTaskRepo.Update(task);
            if (await _context.SaveChangesAsync() > 0) return true;
            var head = task.CreateBy;
            var control = task.Control;
            if (head.Email != null)
            {
                var message = new MailMessage(new string[] { head.Email }, "Control System", $"Control {control.Name}:\n Task {task.Description} Is Done");
                _mailingService.SendMail(message);
            }
            return false;
        }
    }
}