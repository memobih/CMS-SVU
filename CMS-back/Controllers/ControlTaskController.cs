using AutoMapper;
using CMS_back.Consts;
using CMS_back.DTO;
using CMS_back.Interfaces;
using CMS_back.Mailing;
using CMS_back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CMS_back.IGenericRepository;

namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ControlTaskController : ControllerBase
    {

        public IMapper _mapper;
        public IMailingService _mailingService;
        public readonly IControlTaskRepository _controlTaskRepo;
        public readonly IUserRepository userRepo;
        public readonly IGenericRepository<ControlUsers> _controlUserRepo;
        public readonly IGenericRepository<Control> _controlRepo;
        public ControlTaskController(IMapper mapper, IMailingService mailingService,
            IControlTaskRepository repo, IUserRepository userRepo, IGenericRepository<ControlUsers> repo2,
            IGenericRepository<Control> controlRepo)
        {
            _mapper = mapper;
            _mailingService = mailingService;
            _controlTaskRepo = repo;
            _controlUserRepo = repo2;
            this.userRepo = userRepo;
            this._controlRepo = controlRepo;
        }
        [HttpPost("create-task")]
        public async Task<IActionResult> create(controlTaskDTO controlTaskDTO, string Cid)
        {
            var task = await _controlTaskRepo.Create(_mapper.Map<Control_Task>(controlTaskDTO), controlTaskDTO.UserTaskIds, Cid);
            if (task == null) return BadRequest("Can't Create Task Try Again later");
            return Ok("Task Created Successfully");
        }

        [HttpPut("update-task")]
        public async Task<IActionResult> UpdateTask(controlTaskDTO controlTaskDTO, string Tid)
        {
            if (controlTaskDTO == null) return BadRequest("Invalid Task data");
            var temp = _mapper.Map<Control_Task>(controlTaskDTO);
            temp.Id = Tid;

            var task = await _controlTaskRepo.UpdateTask(temp, controlTaskDTO.UserTaskIds);
            if (task == null) return BadRequest("Task Not Found");

            return Ok("Task Updated Successfully");
        }

        [HttpGet("get-tasks-by-control-id")]
        public async Task<IActionResult> GetTasksByControlId(string Cid)
        {
            var tasks = await _controlTaskRepo.GetTasksOfControl(Cid);
            if (tasks == null) return BadRequest("Not Found Tasks");
            return Ok(tasks);
        }

        [HttpGet("user/{userId}/{controlId}")]
        public async Task<IActionResult> GetUserTasks(string controlId, string userId)
        {
            var tasks = await _controlTaskRepo.GetUserTasks(controlId, userId);
            if (tasks == null) return BadRequest("Not Found Tasks");
            return Ok(tasks);
        }

        [HttpDelete("delete-task")]
        public async Task<IActionResult> DeleteTask(string Tid)
        {
            var task = await _controlTaskRepo.DeleteTask(Tid);
            return task ? Ok("Task Deleted Successfully") : BadRequest("Can Not Delete This Task");
        }

        [HttpPut("isDone")]
        public async Task<IActionResult> isDone(string Tid)
        {
            var task = await _controlTaskRepo.FinishTask(Tid);
            if (task == null) return BadRequest("Not Found Task");
            return Ok("Task Is Done");
        }
    }
}