using AutoMapper;
using CMS_back.Consts;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.Interfaces;
using CMS_back.Models;
using CMS_back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repoUser;
        private readonly IMapper _mapper;

        public UsersController(IMapper mapper ,IUserRepository repo) {
            _mapper=mapper;
            _repoUser = repo;
        }


        [HttpGet("user-for-faculty")]
        public async Task<IActionResult> GetUserForFaculty(string id)
        {
            var users = await _repoUser.GetFacultyUsers(id);
            if (users == null) return Ok(new List<ApplicationUser>());
            var usersResult = users.Select(user => _mapper.Map<UserResultDto>(user));
            return usersResult !=  null ? Ok(usersResult):BadRequest("there is no users");
        }

        [HttpGet("user-for-control")]
        public async Task<IActionResult> GetUserForControl(string controlId)
        {
            var usersResult = await _repoUser.GetControlUsers(controlId);
            if (usersResult == null) return BadRequest("Control not found");
            var resultUsers = _mapper.Map<List<ContorlAndItsUserJobTitleDTO>>(usersResult);
            return Ok(resultUsers);
        }

        [HttpGet("current-user")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _repoUser.GetCurrentUser();
            if (user == null) return BadRequest("No user Login yet");
            var userDto=new UserResultDto { Id=user.Id, Name=user.Name, FaculityEmployeeID = user.FaculityEmployeeID, FaculityLeaderID = user.FaculityLeaderID};
            return Ok(userDto);
        }

        [HttpGet("controls/{Uid}")]
        public async Task<IActionResult> getUserControlsAndRoles([FromRoute]string Uid)
        {
            List<ControlUsers>? control_user = await _repoUser.GetUserOfControl(Uid);
            if (control_user == null) return BadRequest("User not register in any control");
            var resultUser = _mapper.Map<List<UserWithHisControlDTO>>(control_user);
            return Ok(resultUser);
        }

        [HttpGet("headConrol/{Cid}")]
        public async Task<IActionResult> headOfControl (string Cid)
        {
            var controlHead = await _repoUser.GetHeadOfControl(Cid);
            if (controlHead == null) return BadRequest("Not Found Head");
            return Ok(controlHead);
        }
    }
}
