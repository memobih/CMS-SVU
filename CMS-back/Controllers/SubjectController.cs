using AutoMapper;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        public IHttpContextAccessor ContextAccessor { get; }
        public UserManager<ApplicationUser> Usermanager { get; }

        private readonly IMapper _mapper;
        public SubjectController(CMSContext _context, IMapper mappe, IHttpContextAccessor contextAccessor,
            UserManager<ApplicationUser> usermanager) 
        {
            Context=_context;
            _mapper=mappe;
            ContextAccessor=contextAccessor;
            Usermanager=usermanager;
        }


        [HttpGet("subject-by-id")]
        public async Task<IActionResult> GetSubjectForFaculty(string sId)
        {
            //var facultyNode = Context.Faculity_Node.Where(x => x.FaculityNodeID == id).ToList();
            //List<subjectResultDTO>? subjects = new List<subjectResultDTO>();
            //foreach(var  faculty_node in facultyNode)
            //{
            //    var subject_Node = Context.Subject.Where(s => s.FaculityNodeID == faculty_node.Id);
            //    foreach(var subject in subject_Node)
            //    {
            //        subjectResultDTO subjectdto = new subjectResultDTO()
            //        {
            //            id = subject.Id,
            //            Name = subject.Name,
            //            Code = subject.Code,
            //            Credit_Hours = subject.Credit_Hours
            //        };
            //        subjects.Add(subjectdto);
            //    }
            //}
            //if (subjects == null) return Ok(new List<Subject>());
            var subject=Context.Subject.FirstOrDefault(x => x.Id==sId);
            var subjectDto=_mapper.Map<subjectResultDTO>(subject);
            return Ok(subjectDto);
        }

        [HttpGet("subjects-of-control")]
        public async Task<IActionResult> GetSubjectForControl(string controld)
        {
            var controlSubjects = Context.ControlSubject.Include(c=>c.Subject).Where(cs => cs.ControlID == controld).ToList();
            var subjects=controlSubjects.Select(x=>x.Subject);
            var subjectsResult= subjects.Select(subject => _mapper.Map<subjectResultDTO>(subject)).ToList();
            return Ok(subjectsResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> create(subjectDTO subjectdto)
        {
            var fn = Context.Faculity_Node.FirstOrDefault(fc => fc.Id == subjectdto.FaculityNodeID);
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

        [HttpPut("isDone/{Sid}")]
        public async Task<IActionResult> IsDone(string Sid)
        {
            var user = ContextAccessor.HttpContext.User;
            var currentUser = await Usermanager.GetUserAsync(user);
            var isHead = Context.ControlUsers.FirstOrDefault(u => u.UserID == currentUser.Id);
            if (isHead == null || isHead.JobType != JobType.Head) return BadRequest("Head of control only has access");
            var subject = Context.Subject.FirstOrDefault(s => s.Id == Sid);
            if (subject == null) return BadRequest("Subject not found");
            subject.IsDone = Question.Yes;
            await Context.SaveChangesAsync();
            return Ok("updated subejct");
        }
        [HttpPut("isReview/{Sid}")]
        public async Task<IActionResult> IsReview(string Sid)
        {
            var user = ContextAccessor.HttpContext.User;
            var currentUser = await Usermanager.GetUserAsync(user);
            var isHead = Context.ControlUsers.FirstOrDefault(u => u.UserID == currentUser.Id);
            if (isHead == null || isHead.JobType != JobType.Head) return BadRequest("Head of control only has access");
            var subject = Context.Subject.FirstOrDefault(s => s.Id == Sid);
            if (subject == null) return BadRequest("Subject not found");
            subject.IsReview = Question.Yes;
            await Context.SaveChangesAsync();
            return Ok("updated subejct");
        }
    }
}
