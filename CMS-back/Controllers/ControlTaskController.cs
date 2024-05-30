using AutoMapper;
using CMS_back.Consts;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.Interfaces;
using CMS_back.Mailing;
using CMS_back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMS_back.IGenericRepository;
using System.Security.Cryptography;

namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ControlTaskController : ControllerBase
    {
        public IMapper _mapper;
        public IMailingService MailingService;
        public readonly IControlTaskRepository controlTaskRepo;
        public readonly IUserRepository userRepo;
        public readonly IGenericRepository<ControlUsers> controlUserRepo;
        public readonly IGenericRepository<Control> controlRepo;
        public ControlTaskController(IMapper mapper, IMailingService mailingService, 
            IControlTaskRepository repo, IUserRepository userRepo, IGenericRepository<ControlUsers> repo2,
            IGenericRepository<Control> controlRepo)
        {
            _mapper=mapper;
            MailingService=mailingService;
            controlTaskRepo = repo;
            controlUserRepo = repo2;
            this.userRepo=userRepo;
            this.controlRepo=controlRepo;
        }

        [HttpPost("create-task")]
        public async Task<IActionResult> create(controlTaskDTO controlTaskDTO, string Cid)
        {
            var currentUser = await userRepo.GetCurrentUser();
            
            var control = await controlRepo.GetById(Cid);
            if (control == null) return BadRequest("Controll not found");
            
            var isHead = await controlUserRepo.FindFirstAsync(
                controlUser => controlUser.UserID == currentUser.Id && controlUser.ControlID == control.Id
                );
            if (isHead == null || isHead.JobType != JobType.Head) 
                return BadRequest("Head of control only has access");

            var task = await controlTaskRepo.Create(_mapper.Map<Control_Task>(controlTaskDTO),
                controlTaskDTO.UserTaskIds, Cid);

            if (task == null) return BadRequest("Can't create task try again later");

            foreach (var userTask in task.UserTasks)
            {
                if (userTask.UserTask.Email != null)
                {
                    var message = new Mailing.MailMessage(new string[] { userTask.UserTask.Email }, "Control System", $"Control {control.Name}:\n Head of control {currentUser.Name} assign new task to you.");
                    MailingService.SendMail(message);
                }
            }
            return Ok("Task created");
        }

        [HttpGet("get-tasks-by-control-id")]
        public async Task<IActionResult> GetTaskByControlId(string Cid)
        {
            var currentUser = await userRepo.GetCurrentUser();
            if (currentUser == null) return BadRequest("No user Login yet");
            var tasks = await controlTaskRepo.GetTasksOfControl(Cid);
            if (tasks == null) return BadRequest("Not Found Tasks");
            var results = tasks.Select(task => _mapper.Map<ControlTaskResultDTO>(task));
            return Ok(results);
        }

        [HttpGet("user/{userId}/{controlId}")]
        public async Task<IActionResult> GetUserTasks(string controlId,string userId)
        {
            var tasks = await controlTaskRepo.GetUserTasks(controlId, userId);
            if (tasks == null) return BadRequest("Not Found Tasks");
            var results = tasks.Select(task => _mapper.Map<ControlTaskResultDTO>(task));
            return Ok(results);
        }

        [HttpPut("update-task")]
        public async Task<IActionResult> UpdateTask(controlTaskDTO controlTaskDTO, string Tid)
        {

            var temp = _mapper.Map<Control_Task>(controlTaskDTO);
            temp.Id = Tid;

            var task = await controlTaskRepo.UpdateTask(temp,
                controlTaskDTO.UserTaskIds);
            if (task == null) return BadRequest("Task Not Found");
            var control = task.Control;
            var creator = task.CreateBy;

            var isHead = await controlUserRepo.FindFirstAsync(
                user => user.UserID == creator.Id && user.ControlID == control.Id);

            if (isHead == null || isHead.JobType != JobType.Head) return BadRequest("Head of control only has access");


            foreach (var user in task.UserTasks)
            {
                if (user.UserTask.Email != null)
                {
                    var message = new Mailing.MailMessage(new string[] { user.UserTask.Email }, "Control System", $"Control {control.Name}:\n Head of control {creator.Name} assign new task to you.");
                    MailingService.SendMail(message);
                }
            }
            return Ok("Update Data");
        }

        [HttpDelete("delete-task")]
        public async Task<IActionResult> DeleteTask(string Tid)
        {
            
            var currentUser = await userRepo.GetCurrentUser();

            if (await controlTaskRepo.GetTaskByID(Tid) == null)
                return BadRequest("Not Found Task");

            var control = (await controlTaskRepo.GetTaskByID(Tid)).Control;
            var isHead = await controlUserRepo.FindFirstAsync(
                user => user.UserID == currentUser.Id && user.ControlID == control.Id);
           
            if (isHead == null || isHead.JobType != JobType.Head) return BadRequest("Head of control only has access");

            var task = await controlTaskRepo.DeleteTask(Tid);
            return Ok("Task Deleted");
        }

        [HttpPut("isDone")]
        public async Task<IActionResult> isDone(string Tid)
        {
            var task = await controlTaskRepo.FinishTask(Tid);
            if (task == null) return BadRequest("Not Found Task");
            var head = task.CreateBy;
            var control = task.Control;
            if (head.Email != null)
            {
                var message = new Mailing.MailMessage(new string[] { head.Email }, "Control System", $"Control {control.Name}:\n Task {task.Description} is Done");
                MailingService.SendMail(message);
            }
            return Ok("Done");
        }
    }
}
