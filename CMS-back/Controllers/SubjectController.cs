using CMS_back.Data;
using CMS_back.Reposatory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectController : ControllerBase
    {

        public SubjectController(CMSContext _context) 
        {
            Context=_context;
        }

        public CMSContext Context { get; }

        [HttpGet("facult/{id}")]
        public async Task<IActionResult> getSubjectForFaculty(string id)
        {
            List<Subject>? subjects = Context.Subjects.Where(s => s.ID == id).ToList();
            if (subjects == null) return Ok(new List<Subject>());
            return Ok(subjects);
        }



    }
}
