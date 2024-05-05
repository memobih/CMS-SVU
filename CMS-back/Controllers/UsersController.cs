using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.Reposatory.Models;
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

        public UsersController(CMSContext _context, UserManager<ApplicationUser> usermanager
            , IHttpContextAccessor contextAccessor) {
            context=_context;
            Usermanager=usermanager;
            ContextAccessor=contextAccessor;
        }


        // get user for specfic faculty
        //[Authorize(Roles = "FaculityAdministrator")]
        [HttpGet("user-for-faculty/{id}")]
        public async Task<IActionResult> GetUserForFaculty(string id)
        {
            List<ApplicationUser>? users = await context.Users.Where(u => u.FaculityEmployeeID == id).ToListAsync();
            if (users == null) return Ok(new List<ApplicationUser>());
            return Ok(users);
        }

        [HttpGet("user-for-control/{id}")]
        public IActionResult GetUserForControl(string id)
        {
            var control = context.Control.FirstOrDefault(c => c.Id== id);
            if (control == null) return BadRequest("Control not found");
            List<ApplicationUser>? users = context.Users.Where(u => u.UserCreatorControls.Contains(control)).ToList();
            if (users == null) return Ok(new List<ApplicationUser>());
            return Ok(users);
        }

        [HttpGet("current-user")]
        public async Task<IActionResult> GetUser()
        {
            var user = ContextAccessor.HttpContext.User;
            if (user == null) return BadRequest("No user Login yet");
            var currentUser = await Usermanager.GetUserAsync(user);
            var userDto=new UserResultDto { Id=currentUser.Id, Name=currentUser.Name };
            return Ok(userDto);
        }

    }
}
