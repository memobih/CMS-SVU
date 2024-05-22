using Microsoft.AspNetCore.Mvc;
using CMS_back.DTO;
using CMS_back.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CMS_back.Models;
using AutoMapper;
using CMS_back.Consts;
using CMS_back.Mailing;
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


        [HttpPost("create/{Fid}")]
        [Authorize(Roles = ConstsRoles.AdminFaculty)]
        public async Task<IActionResult> createControl(ControlDTO controldto, string Fid)
        {
            var result = await _repo.AddAsync(controldto, Fid);
            return result ? Ok(result) : BadRequest("Invalid Control Data");
        }

        [HttpPut("edit")]
        [Authorize(Roles = ConstsRoles.AdminFaculty)]
        public async Task<IActionResult> EditControl(ControlDTO controldto, string Cid)
        {
            var result = await _repo.UpdateAsync(controldto, Cid);
            return result ? Ok(result) : BadRequest("Invalid Control Data");
        }

        [HttpGet("allControllers")]
        public async Task<IActionResult> index()
        {
            var controls = await _repo.GetAllAsync();
            return Ok(controls);
        }

        [HttpGet("detail")]
        public async Task<IActionResult> detail(string id)
        {
            var control = await _repo.GetByIdAsync(id);
            if (control == null) return BadRequest("Not Found");
            return Ok(control);
        }

        [HttpDelete("delete")]
        [Authorize(Roles = ConstsRoles.AdminFaculty)]
        public async Task<IActionResult> delete(string id)
        {
            var result = await _repo.DeleteAsync(id);
            return result ? Ok("Deleted Successfuly") : BadRequest("Can Not Delete This Control");
        }

        [HttpGet("get-by-faculity-id")]
        public async Task<IActionResult> get(string Fid)
        {
            var controls = await _repo.GetControlsByFaculityIdAsync(Fid);
            return Ok(controls);
        }

        [HttpGet("acadmec-year")]
        public async Task<IActionResult> GetControlsAcademcYear(string AcadYear)
        {
            var controls = await _repo.GetControlsByAcadYearAsync(AcadYear);
            return Ok(controls);
        }
    }
}
