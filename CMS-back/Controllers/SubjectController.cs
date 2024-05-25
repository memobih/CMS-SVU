using AutoMapper;
using CMS_back.Consts;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.Interfaces;
using CMS_back.Models;
using CMS_back.Services;
using CMS_back.IGenericRepository;
using CMS_back.GenericRepository;
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
        private readonly IMapper _mapper;
        private readonly ISubjectRepository subjectRepo;
        private readonly IUserRepository userRepo;

        public IGenericRepository<Faculity_Node> facultyNodeRepo { get; }

        public SubjectController(IMapper mappe, ISubjectRepository repo, IUserRepository repo2
            ,IGenericRepository<Faculity_Node> repo3) 
        {
            _mapper=mappe;
            subjectRepo = repo;
            userRepo = repo2;
            facultyNodeRepo = repo3;
        }


        [HttpGet("faculty/{Fid}")]
        public async Task<IActionResult> GetSubjectForFaculty(string Fid)
        {
            var subjects = await subjectRepo.GetFacultySubject(Fid);
            if (subjects == null) return BadRequest("Not Found subjects");
            var subjectsResult = subjects.Select(s => _mapper.Map<subjectResultDTO>(s)).ToList();
            return Ok(subjectsResult);
        }

        [HttpGet("subjects-of-control")]
        public async Task<IActionResult> GetSubjectForControl(string controld)
        {

            var subjects = await subjectRepo.GetControlSubject(controld);
            if (subjects == null) return BadRequest("Not found Subjects");
            var subjectsResult = subjects.Select(subject => _mapper.Map<subjectResultDTO>(subject)).ToList();
            return Ok(subjectsResult);
        }

        [HttpPost("add")]
        public async Task<IActionResult> create(subjectDTO subjectdto)
        {
            var facultyNode = await facultyNodeRepo.GetById(subjectdto.FaculityNodeID);
            if (facultyNode == null) return BadRequest("Faculty node not found");
            Subject subject = _mapper.Map<Subject>(subjectdto);
            if (await subjectRepo.AddSubject(subject) == null)
                return BadRequest("Subject entered before");
            return Ok("subject Added");
        }

        [HttpPut("isDone/{Sid}")]
        public async Task<IActionResult> IsDone(string Sid)
        {
            var currentUser = await userRepo.GetCurrentUser();
            if (await subjectRepo.FinishedSubject(Sid) == null)
                return BadRequest("Subject not found");
            return Ok("updated subejct");
        }
        [HttpPut("isReview/{Sid}")]
        public async Task<IActionResult> IsReview(string Sid)
        {
            var currentUser = await userRepo.GetCurrentUser();
            if (await subjectRepo.ReviewSubject(Sid) == null) 
                return BadRequest("Subject not found");
            return Ok("updated subejct");
        }
    }
}
