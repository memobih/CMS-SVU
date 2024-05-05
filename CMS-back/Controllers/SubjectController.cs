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
    public class SubjectController : ControllerBase
    {

        public CMSContext Context { get; }
        public SubjectController(CMSContext _context) 
        {
            Context=_context;
        }


        [HttpGet("facult/{id}")]
        public async Task<IActionResult> getSubjectForFaculty(string id)
        {
            var facultyNode = Context.Faculity_Nodes.Where(x => x.FaculityNodeID == id).ToList();
            List<subjectResultDTO>? subjects = new List<subjectResultDTO>();
            foreach(var  faculty_node in facultyNode)
            {
                var subject_Node = Context.Subject.Where(s => s.FaculityNodeID == faculty_node.Id);
                foreach(var subject in subject_Node)
                {
                    subjectResultDTO subjectdto = new subjectResultDTO()
                    {
                        id = subject.Id,
                        Name = subject.Name,
                        Code = subject.Code,
                        Credit_Hours = subject.Credit_Hours
                    };
                    subjects.Add(subjectdto);
                }
            }
            if (subjects == null) return Ok(new List<Subject>());
            return Ok(subjects);
        }

        [HttpGet("control/{id}")]
        public async Task<IActionResult> getSubjectForControl(string id)
        {
            var control_subjects = Context.ControlSubject.Where(cs => cs.ControlID == id).ToList();
            List<Subject> subjects = new List<Subject>();
            foreach (var cs in control_subjects)
            {
                var subject = Context.Subject.FirstOrDefault(s => cs.SubjectID == s.Id);
                if(subject == null) continue;
                subjects.Add(subject);
            }
            return Ok(subjects);
        }

        [HttpPost("add")]
        public async Task<IActionResult> create(subjectDTO subjectdto)
        {
            var fn = Context.Faculity_Nodes.FirstOrDefault(fc => fc.Id == subjectdto.FaculityNodeID);
            if (fn == null) return BadRequest("Faculty node not found");
            var isExict = Context.Subject.FirstOrDefault(s => s.Code == subjectdto.Code);
            if (isExict != null) return BadRequest("Subject entered before");
            Subject subject = new Subject()
            {
                Name = subjectdto.Name,
                Code = subjectdto.Code,
                Credit_Hours = subjectdto.Credit_Hours,
                FaculityNodeID = subjectdto.FaculityNodeID,
            };
            fn.Subjects.Add(subject);
            Context.Subject.Add(subject);
            
            await Context.SaveChangesAsync();
            return Ok("subject Added");
        }
    }
}
