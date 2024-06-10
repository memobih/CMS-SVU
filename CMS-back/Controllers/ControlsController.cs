using Microsoft.AspNetCore.Mvc;
using CMS_back.DTO;
using Microsoft.AspNetCore.Authorization;
using CMS_back.Consts;
using CMS_back.Interfaces;

namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ControlsController : ControllerBase
    {
        private readonly IControlRepository _repo;
        public ControlsController(IControlRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("create/{fid}")]
        [Authorize(Roles = ConstsRoles.AdminFaculity)]
        public async Task<IActionResult> createControl(ControlDTO controldto, string fid)
        {
            var result = await _repo.AddAsync(controldto, fid);
            return result ? Ok("Control Added Successfully") : BadRequest("Invalid Control Data");
        }

        [HttpPut("edit/{cid}")]
        [Authorize(Roles = ConstsRoles.AdminFaculity)]
        public async Task<IActionResult> EditControl(ControlDTO controldto, string cid)
        {
            var result = await _repo.UpdateAsync(controldto, cid);
            return result ? Ok("Updated Successfully") : BadRequest("Invalid Control Data");
        }

        [HttpGet("get-all-controls")]
        public async Task<IActionResult> Index()
        {
            var controls = await _repo.GetAllAsync();
            return Ok(controls);
        }

        [HttpGet("details/{cid}")]
        public async Task<IActionResult> Detail(string cid)
        {
            var control = await _repo.GetByIdAsync(cid);
            if (control == null) return BadRequest("Not Found");
            return Ok(control);
        }

        [HttpDelete("delete/{cid}")]
        [Authorize(Roles = ConstsRoles.AdminFaculity)]
        public async Task<IActionResult> delete(string cid)
        {
            var result = await _repo.DeleteAsync(cid);
            return result ? Ok("Deleted Successfully") : BadRequest("Can Not Delete This Control");
        }

        [HttpGet("get-controls-by-faculity-id/{fid}")]
        public async Task<IActionResult> GetControlsByFaculityId(string fid)
        {
            var controls = await _repo.GetControlsByFaculityIdAsync(fid);
            return Ok(controls);
        }

        [HttpGet("get-controls-by-academic-year")]
        public async Task<IActionResult> GetControlsByAcademcYear(string acadYear)
        {
            var controls = await _repo.GetControlsByAcadYearAsync(acadYear);
            return Ok(controls);
        }
    }
}
