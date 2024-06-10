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
        private readonly IMapper _mapper;
        private readonly IUserHelpers _userHelpers;
        private readonly IMailingService _mailingService;
        private readonly IGenericRepository<Control_Task> _controlTaskRepo;
        private readonly IGenericRepository<ControlUsers> _controlUserRepo;
        private readonly IGenericRepository<Control_UserTasks> _controlUserTasksrepo;
        private readonly IGenericRepository<Control> _controlRepo;

        public ControlTaskRepository(CMSContext context, IUserHelpers userHelpers, IMapper mapper, IGenericRepository<Control_Task> genericRepository
            , IMailingService mailingService, IGenericRepository<Control_UserTasks> controlUserTasksrepo
            , IGenericRepository<Control> controlRepo, IGenericRepository<ControlUsers> controlUserRepo)
        {
            _context = context;
            _controlTaskRepo = genericRepository;
            _mailingService = mailingService;
            _controlUserRepo = controlUserRepo;
            _controlRepo = controlRepo;
            _userHelpers = userHelpers;
            _controlUserTasksrepo = controlUserTasksrepo;
            _mapper = mapper;
        }

        public async Task<bool> Create(Control_Task task, List<string> usersTasksIds, string controlId)
        {
            var currentUser = await _userHelpers.GetCurrentUserAsync();

            var control = await _controlRepo.GetById(controlId);
            if (control == null) throw new Exception("Control Not Found");

            var isHead = await _controlUserRepo.FindFirstAsync(controlUser => controlUser.UserID == currentUser.Id && controlUser.ControlID == control.Id);
            if (isHead == null || isHead.JobType != JobType.Head) throw new Exception("Head of Control Only Has Access");

            task.CreationDate = DateOnly.FromDateTime(DateTime.Now);
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
            if (await _context.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<bool> UpdateTask(Control_Task taskToUpdate, List<string> usersTasksIds)
        {
            var task = await _controlTaskRepo.FindFirstAsync(
                ts => ts.Id == taskToUpdate.Id,
                new[] { "Control", "CreateBy", "UserTasks", "UserTasks.UserTask" }
            );

            if (task == null) throw new Exception("ControlTasks Not Found");

            task.Description = taskToUpdate.Description;
            task.CreationDate = DateOnly.FromDateTime(DateTime.Now);
            task.CreateBy = await _userHelpers.GetCurrentUserAsync();

            if (task.UserTasks == null)
            {
                await _context.Entry(task).Collection(t => t.UserTasks).LoadAsync();
            }

            var tasksToRemove = task.UserTasks.Where(ut => !usersTasksIds.Contains(ut.UserTaskID)).ToList();
            foreach (var taskToRemove in tasksToRemove)
            {
                _context.Remove(taskToRemove);
            }

            foreach (var userId in usersTasksIds)
            {
                if (!task.UserTasks.Any(ut => ut.UserTaskID == userId))
                {
                    var newUserTask = new Control_UserTasks
                    {
                        Control_TaskID = task.Id,
                        UserTaskID = userId
                    };
                    task.UserTasks.Add(newUserTask);
                }
            }

            if (await _context.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<IEnumerable<ControlTaskResultDTO>> GetTasksOfControl(string controlId)
        {
            var currentUser = await _userHelpers.GetCurrentUserAsync();
            if (currentUser == null) throw new Exception("No user Login yet");
            var tasks = await _controlTaskRepo.FindAsync(c => c.ControlID == controlId, ["UserTasks", "UserTasks.UserTask"]);
            var results = _mapper.Map<IEnumerable<ControlTaskResultDTO>>(tasks);
            return results;
        }

        public async Task<IEnumerable<ControlTaskResultDTO>> GetUserTasks(string controlId, string userId)
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

        public async Task<Control_Task> GetTaskByID(string taskId)
        {
            var task = await _controlTaskRepo.FindFirstAsync(ts => ts.Id == taskId, ["Control", "CreateBy", "UserTasks", "UserTasks.UserTask"]);
            return task;
        }

        public async Task<bool> FinishTask(string taskId)
        {
            var userTasks = await _controlUserTasksrepo.FindAsync(ts => ts.Control_TaskID == taskId);
            if (userTasks == null) throw new Exception("This Task Is Not Found");

            foreach (var tempTask in userTasks)
            {
                if (tempTask.Control_TaskID == taskId)
                {
                    tempTask.IsDone = Question.Yes;
                }
                _controlUserTasksrepo.Update(tempTask);
            }
            if (await _context.SaveChangesAsync() > 0)
            { 
            var task = await _controlTaskRepo.FindFirstAsync(ts => ts.Id == taskId, ["Control", "CreateBy", "UserTasks"]);
                var head = task.CreateBy;
                var control = task.Control;
                if (head.Email != null)
                {
                    var message = new MailMessage(new string[] { head.Email }, "Control System", $"Control {control.Name}:\n Task {task.Description} Is Done");
                    _mailingService.SendMail(message);
                }
                return true;
            }
            return false;
        }
    }
}