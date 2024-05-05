using CMS_back.Data;
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

        [HttpGet("control/{id}")]
        public async Task<IActionResult> getSubjectForControl(string id)
        {
            var control_subjects = Context.ControlSubjects.Where(cs => cs.ControlID == id).ToList();
            List<Subject> subjects = new List<Subject>();
            foreach (var cs in control_subjects)
            {
                var subject = Context.Subjects.FirstOrDefault(s => cs.SubjectID == s.ID);
                if(subject == null) continue;
                subjects.Add(subject);
            }
            return Ok(subjects);
        }
    }
}
