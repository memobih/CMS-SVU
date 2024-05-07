using AutoMapper;
using CMS_back.Consts;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.Mailing;
using CMS_back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ControlTaskController : ControllerBase
    {
        public CMSContext Context { get; set; }
        public IMapper Mapper { get; }
        public UserManager<ApplicationUser> Usermanager { get; }
        public IHttpContextAccessor ContextAccessor { get; }
        public IMailingService MailingService { get; }

        public ControlTaskController(CMSContext context, IMapper mapper, UserManager<ApplicationUser> usermanager
            , IHttpContextAccessor contextAccessor, IMailingService mailingService)
        {
            Context=context;
            Mapper=mapper;
            Usermanager=usermanager;
            ContextAccessor=contextAccessor;
            MailingService=mailingService;
        }

        [HttpPost("{Cid}")]
        public async Task<IActionResult> create(controlTaskDTO controlTaskDTO, [FromRoute] string Cid)
        {
            var user = ContextAccessor.HttpContext.User;
            var currentUser = await Usermanager.GetUserAsync(user);
            var isHead = Context.ControlUsers.FirstOrDefault(u => u.UserID == currentUser.Id);
            if (isHead == null || isHead.JobType != JobType.Head) return BadRequest("Head of control only has access");

            var control = Context.Control.FirstOrDefault(c => c.Id == Cid);
            if (control == null) return BadRequest("Controll not found");
            Control_Task control_Task = Mapper.Map<Control_Task>(controlTaskDTO);
            control_Task.CreationDate = DateTime.Now;
            control_Task.CreateBy = currentUser;
            control_Task.Control = control;

            foreach (var userTask in controlTaskDTO.UserTaskIds)
            {
                var ut = Context.Users.FirstOrDefault(u => u.Id == userTask);
                Control_UserTasks control_UserTasks = new Control_UserTasks()
                {
                    Control_Task = control_Task,
                    UserTask = ut,
                };
                if (ut.Email != null)
                {
                    var message = new Mailing.MailMessage(new string[] { ut.Email }, "Control System", $"Control {control.Name}:\n Head of control {currentUser.Name} assign new task to you.");
                    MailingService.SendMail(message);
                }
                Context.Control_UserTasks.Add(control_UserTasks);
                control_Task.UserTasks.Add(control_UserTasks);
            }

            await Context.SaveChangesAsync();
            return Ok("Task created");
        }

        [HttpGet("{Cid}")]
        public async Task<IActionResult> get([FromRoute] string Cid)
        { 
            var tasks = Context.Control_Task.Where(ct => ct.ControlID == Cid).ToList();
            List<ControlTaskResultDTO> results = new List<ControlTaskResultDTO>();
            foreach (var task in tasks)
            {
                var controlTask = Context.Control_UserTasks.Include(c => c.UserTask).Where(c => c.Control_TaskID == task.Id).ToList();
                foreach (var ct in controlTask)
                {
                    results.Add(new ControlTaskResultDTO()
                    {
                        Description = task.Description,
                        user = ct.UserTask
                    });
                }
            }
            return Ok(results);
        }

        [HttpPut("{Tid}")]
        public async Task<IActionResult> update(controlTaskDTO controlTaskDTO,[FromRoute]string Tid)
        {
            
            var task = Context.Control_Task.Include(c => c.Control).Include(c => c.CreateBy).FirstOrDefault(t => t.Id == Tid);
            task.Description = controlTaskDTO.Description;
            var control = task.Control;
            var currentUser = task.CreateBy;
            task.CreationDate = DateTime.Now;
            foreach (var userTask in controlTaskDTO.UserTaskIds)
            {
                var ut = Context.Users.FirstOrDefault(u => u.Id == userTask);
                Control_UserTasks control_UserTasks = new Control_UserTasks()
                {
                    Control_Task = task,
                    UserTask = ut,
                };
                if (ut.Email != null)
                {
                    var message = new Mailing.MailMessage(new string[] { ut.Email }, "Control System", $"Control {control.Name}:\n Head of control {currentUser.Name} assign new task to you.");
                    MailingService.SendMail(message);
                }
                Context.Control_UserTasks.Add(control_UserTasks);
                task.UserTasks.Add(control_UserTasks);
            }
            await Context.SaveChangesAsync();
            return Ok("Update Data");
        }

        [HttpDelete("{Tid}")]
        public async Task<IActionResult> delete([FromRoute]string Tid)
        {
            var user = ContextAccessor.HttpContext.User;
            var currentUser = await Usermanager.GetUserAsync(user);
            var isHead = Context.ControlUsers.FirstOrDefault(u => u.UserID == currentUser.Id);
            if (isHead == null || isHead.JobType != JobType.Head) return BadRequest("Head of control only has access");

            var task = Context.Control_Task.FirstOrDefault(t => t.Id == Tid);
            Context.Control_Task.Remove(task);
            await Context.SaveChangesAsync();
            return Ok("Task Deleted");
        }

        [HttpPut("isDone")]
        public async Task<IActionResult> isDone(string Tid)
        {   
            var task = Context.Control_Task.Include(c => c.CreateBy).Include(c => c.Control).FirstOrDefault(t => t.Id == Tid);
            if (task == null) return BadRequest("Not Found Task");
            task.IsDone = Question.Yes;
            var head = task.CreateBy;
            var control = task.Control;
            if (head.Email != null)
            {
                var message = new Mailing.MailMessage(new string[] { head.Email }, "Control System", $"Control {control.Name}:\n Task {task.Description} is Done");
                MailingService.SendMail(message);
            }
            await Context.SaveChangesAsync();
            return Ok("Done");
        }
    }
}
