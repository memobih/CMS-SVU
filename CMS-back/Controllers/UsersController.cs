using AutoMapper;
using CMS_back.Consts;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.Models;
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

        public CMSContext context { get; }
        public UserManager<ApplicationUser> Usermanager { get; }
        public IHttpContextAccessor ContextAccessor { get; }
        private readonly IMapper _mapper;

        public UsersController(CMSContext _context, UserManager<ApplicationUser> usermanager
            , IHttpContextAccessor contextAccessor, IMapper mapper) {
            context=_context;
            Usermanager=usermanager;
            ContextAccessor=contextAccessor;
            _mapper = mapper;
        }


        // get user for specfic faculty
        //[Authorize(Roles = "FaculityAdministrator")]
        [HttpGet("user-for-faculty")]
        public async Task<IActionResult> GetUserForFaculty(string id)
        {
            var faculty = context.Faculity.Include(f=>f.Users).FirstOrDefault(f => f.Id == id);
            var users=faculty.Users.ToList();
            if (users == null) return Ok(new List<ApplicationUser>());
            var usersResult = users.Select(user => _mapper.Map<UserResultDto>(user));
            return usersResult!= null? Ok(usersResult):BadRequest("ther is no users");
        }

        [HttpGet("user-for-control")]
        public IActionResult GetUserForControl(string controlId)
        {
            var controlsUser = context.ControlUsers.Include(c=>c.Control).Where(c => c.ControlID== controlId);
            if (controlsUser == null) return BadRequest("Control not found");
            List<ApplicationUser>? users = controlsUser.Select(c=>c.User).ToList();
            if (users == null) return Ok(new List<ApplicationUser>());
            var usersResult=users.Select(user => _mapper.Map<UserResultDto>(user)).ToList();
            return Ok(usersResult);
        }

        [HttpGet("current-user")]
        public async Task<IActionResult> GetUser()
        {
            var user = ContextAccessor.HttpContext.User;
            if (user == null) return BadRequest("No user Login yet");
            var currentUser = await Usermanager.GetUserAsync(user);
            string facultyid;
            if (currentUser.FaculityLeaderID != null) facultyid = currentUser.FaculityLeaderID;
            else facultyid = currentUser.FaculityEmployeeID;
            var userDto=new UserResultDto { Id=currentUser.Id, Name=currentUser.Name, facultyID = facultyid};
            return Ok(userDto);
        }

        [HttpGet("controls/{Uid}")]
        public async Task<IActionResult> getUserControlsAndRoles([FromRoute]string Uid)
        {
            var control_user = context.ControlUsers.Include(c => c.User).Include(c => c.Control).Where(uc => uc.UserID== Uid).ToList();
            if (control_user == null) return BadRequest("User not register in any control");
            var resultUser = _mapper.Map<List<UserWithHisControlDTO>>(control_user);
            return Ok(resultUser);
        }

        [HttpGet("headConrol/{Cid}")]
        public IActionResult headOfControl (string Cid)
        {
            var controlHead = context.ControlUsers.Include(u => u.User).FirstOrDefault(c => c.ControlID == Cid && c.JobType == JobType.Head);
            if (controlHead == null) return BadRequest("Not Found Head");
            return Ok(controlHead);
        }
    }
}
