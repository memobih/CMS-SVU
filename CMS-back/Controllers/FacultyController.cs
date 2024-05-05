using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.Reposatory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class FacultyController : ControllerBase
    {
        public CMSContext context { get; set; }

        public FacultyController(CMSContext context) 
        { 
            this.context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> create(FacultyDTO facultyDTO)
        {
            var isExict = context.Faculities.FirstOrDefault(f => f.Name == facultyDTO.Name);
            if (isExict != null) return BadRequest("Faculty is exict");
            Faculity faculity = new Faculity()
            {
                Name = facultyDTO.Name,
                Code = facultyDTO.Code,
                Order = facultyDTO.Order,
                Type = facultyDTO.Type
            };
            faculity.ID = Guid.NewGuid().ToString();
            var leader = context.Users.FirstOrDefault(u => u.Id == facultyDTO.UserLeaderID);
            if (leader == null) return BadRequest("Must enter Leader Faculty");
            faculity.UserLeader = leader;
            faculity.UserLeaderID = leader.Id;

            context.Faculities.Add(faculity);

            leader.FaculityLeaderID = faculity.ID;
            leader.FaculityLeader = faculity;


            await context.SaveChangesAsync();

            return Ok("added faculty");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllfaculties()
        {
            var faculties = await context.Faculities.ToListAsync();
            return Ok(faculties);
        }

        [HttpGet("{id:alpha}")]
        public async Task<IActionResult> getfaculty(string id)
        {
            var faculty = await context.Faculities.Where(f => f.ID == id).ToListAsync();
            if (faculty == null) return BadRequest("No found Facluty");
            return Ok(faculty);
        }

    }
}
