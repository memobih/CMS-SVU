using CMS_back.DTO;
using CMS_back.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _repo;
        public SubjectController(ISubjectRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("faculty/{Fid}")]
        public async Task<IActionResult> GetSubjectForFaculty(string Fid)
        {
            var subjects = await _repo.GetFacultySubject(Fid);
            if (subjects == null) return BadRequest("Not Found subjects");
            return Ok(subjects);
        }

        [HttpGet("subjects-of-control")]
        public async Task<IActionResult> GetSubjectForControl(string Cid)
        {
            var subjects = await _repo.GetControlSubject(Cid);
            if (subjects == null) return BadRequest("Not found Subjects");
            return Ok(subjects);
        }

        [HttpPost("add")]
        public async Task<IActionResult> create(subjectDTO subjectdto)
        {
            var result = await _repo.AddSubject(subjectdto);
            return result ? Ok("Subject Added Successfully") : BadRequest("Invalid Subject Data");
        }

        [HttpPut("isDone/{Sid}/{Cid}")]
        public async Task<IActionResult> IsDone(string Cid, string Sid)
        {
            var subject = await _repo.FinishedSubject(Cid, Sid);
            if (subject == null) return BadRequest("Subject Not Found");
            return Ok("Subejct Is Done");
        }

        [HttpPut("isReview/{Sid}/{Cid}")]
        public async Task<IActionResult> IsReview(string Cid, string Sid)
        {
            var subject = await _repo.ReviewSubject(Cid, Sid);
            if (subject == null) return BadRequest("Subject Not Found");
            return Ok("Subejct Is Reviewed");
        }
    }
}
