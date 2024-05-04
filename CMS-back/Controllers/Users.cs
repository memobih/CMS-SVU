using CMS_back.Data;
using CMS_back.Reposatory.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {

        public CMSContext context { get; }
        public Users(CMSContext _context) {
            context=_context;
        }


        // get user for specfic faculty
        [HttpGet("faculty/{id:alpha}")]
        public async Task<IActionResult> getUserForFaculty(string id)
        {
            List<ApplicationUser>? users = context.Users.Where(u => u.FaculityEmployeeID == id).ToList();
            if (users == null) return Ok(new List<ApplicationUser>());
            return Ok(users);
        }



    }
}
