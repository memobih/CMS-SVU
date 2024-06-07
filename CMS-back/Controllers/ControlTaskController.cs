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

        private readonly IControlTaskRepository _repo;
        private readonly IMapper _mapper;

        public ControlTaskController(IControlTaskRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost("create-task-/{controlId}")]
        public async Task<IActionResult> create(controlTaskDTO controlTaskDTO, string controlId)
        {
            var task = await _repo.Create(_mapper.Map<Control_Task>(controlTaskDTO), controlTaskDTO.UserTaskIds, controlId);
            return task ? Ok("Task Created Successfully") :BadRequest("Can't Create Task Try Again later") ;
        }

        [HttpPut("update-task/{tid}")]
        public async Task<IActionResult> UpdateTask(controlTaskDTO controlTaskDTO, string tid)
        {
            if (controlTaskDTO == null) return BadRequest("Invalid Task data");
            var temp = _mapper.Map<Control_Task>(controlTaskDTO);
            temp.Id = tid;

            var task = await _repo.UpdateTask(temp, controlTaskDTO.UserTaskIds);            
            return task ? Ok("Task Updated Successfully") : BadRequest("Task Can't Updated");
        }

        [HttpGet("get-tasks-by-control-id/{cid}")]
        public async Task<IActionResult> GetTasksByControlId(string cid)
        {
            var tasks = await _repo.GetTasksOfControl(cid);
            if (tasks == null) return BadRequest("Not Found Tasks");
            return Ok(tasks);
        }

        [HttpGet("user/{userId}/{controlId}")]
        public async Task<IActionResult> GetUserTasks(string controlId, string userId)
        {
            var tasks = await _repo.GetUserTasks(controlId, userId);
            if (tasks == null) return BadRequest("Not Found Tasks");
            return Ok(tasks);
        }

        [HttpDelete("delete-task/{tid}")]
        public async Task<IActionResult> DeleteTask(string tid)
        {
            var task = await _repo.DeleteTask(tid);
            return task ? Ok("Task Deleted Successfully") : BadRequest("Can Not Delete This Task");
        }

        [HttpPut("isDone/{tid}")]
        public async Task<IActionResult> isDone(string tid)
        {
            var task = await _repo.FinishTask(tid);
            if (task == null) return BadRequest("Not Found Task");
            return Ok("Task Is Done");
        }
    }
}