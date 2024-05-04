using CMS_back.Data;
using CMS_back.Reposatory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class Users : ControllerBase
    {

        public CMSContext context { get; }
        public UserManager<ApplicationUser> Usermanager { get; }
        public IHttpContextAccessor ContextAccessor { get; }

        public Users(CMSContext _context, UserManager<ApplicationUser> usermanager
            , IHttpContextAccessor contextAccessor) {
            context=_context;
            Usermanager=usermanager;
            ContextAccessor=contextAccessor;
        }


        // get user for specfic faculty
        [Authorize(Roles = "FaculityAdministrator")]
        [HttpGet("faculty/{id:alpha}")]
        public async Task<IActionResult> getUserForFaculty(string id)
        {
            List<ApplicationUser>? users = context.Users.Where(u => u.FaculityEmployeeID == id).ToList();
            if (users == null) return Ok(new List<ApplicationUser>());
            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> getUser()
        {
            var currentUser = ContextAccessor.HttpContext.User;
            return Ok(await Usermanager.GetUserAsync(currentUser));
        }

    }
}
