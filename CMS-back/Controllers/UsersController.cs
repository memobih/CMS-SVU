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
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper, IUserRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }


        [HttpGet("user-for-faculty")]
        public async Task<IActionResult> GetUserForFaculty(string id)
        {
            var users = await _repo.GetFacultyUsers(id);
            if (users == null) return Ok(new List<ApplicationUser>());
            return users != null ? Ok(users) : BadRequest("there is no users");
        }

        [HttpGet("user-for-control")]
        public async Task<IActionResult> GetUserForControl(string controlId)
        {
            var usersResult = await _repo.GetControlUsers(controlId);
            if (usersResult == null) return BadRequest("Control not found");
            return Ok(usersResult);
        }

        [HttpGet("current-user")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _repo.GetCurrentUser();
            return Ok(user);
        }

        [HttpGet("controls/{Uid}")]
        public async Task<IActionResult> getUserControlsAndRoles([FromRoute] string Uid)
        {
            List<UserWithHisControlDTO>? control_user = await _repo.GetUserOfControl(Uid);
            return Ok(control_user);
        }

        [HttpGet("headConrol/{Cid}")]
        public async Task<IActionResult> headOfControl(string Cid)
        {
            var controlHead = await _repo.GetHeadOfControl(Cid);
            if (controlHead == null) return BadRequest("Not Found Head");
            return Ok(controlHead);
        }
    }
}
