using AutoMapper;
using CMS_back.DTO;
using CMS_back.Interfaces;
using CMS_back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("get-user-for-faculity/{fid}")]
        public async Task<IActionResult> GetUserForFaculity(string fid)
        {
            var users = await _repo.GetFaculityUsers(fid);
            if (users == null) throw new Exception("No Users are Exist");
            return users != null ? Ok(users) : BadRequest("there is no users");
        }

        [HttpGet("get-users-for-control")]
        public async Task<IActionResult> GetUserForControl(string controlId)
        {
            var usersResult = await _repo.GetControlUsers(controlId);
            if (usersResult == null) return BadRequest("Control not found");
            return Ok(usersResult);
        }

        [HttpGet("current-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _repo.GetCurrentUser();
            if (user == null) return BadRequest("Current User Not Found");
            return Ok(user);
        }

        [HttpGet("get-controls-for-user/{uid}")]
        public async Task<IActionResult> getUserControlsAndRoles([FromRoute] string uid)
        {
            List<UserWithHisControlDTO> control_user = await _repo.GetControlsForUser(uid);
            if (control_user == null) return BadRequest("No ConstrolUsers are Exist");
            return Ok(control_user);
        }

        [HttpGet("get-head-of-control/{cid}")]
        public async Task<IActionResult> headOfControl(string cid)
        {
            var controlHead = await _repo.GetHeadOfControl(cid);
            if (controlHead == null) return BadRequest("Not Found Head");
            return Ok(controlHead);
        }
    }
}
