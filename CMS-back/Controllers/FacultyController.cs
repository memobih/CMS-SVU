using AutoMapper;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.Models;
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
        private readonly IMapper _mapper;
        public FacultyController(CMSContext context, IMapper mappe) 
        { 
            this.context = context;
            _mapper = mappe;
        }

        [HttpPost("add")]
        public async Task<IActionResult> create(FacultyDTO facultyDTO)
        {
            var isExict = context.Faculity.FirstOrDefault(f => f.Name == facultyDTO.Name);
            if (isExict != null) return BadRequest("Faculty is exict");
            Faculity faculity = new Faculity()
            {
                Name = facultyDTO.Name,
                Code = facultyDTO.Code,
                Order = facultyDTO.Order,
            };
            var leader = context.Users.FirstOrDefault(u => u.Id == facultyDTO.UserLeaderID);
            if (leader == null) return BadRequest("Must enter Leader Faculty");
            faculity.UserLeader = leader;
            faculity.UserLeaderID = leader.Id;


            context.Faculity.Add(faculity);

            leader.FaculityLeaderID = faculity.Id;
            leader.FaculityLeader = faculity;


            await context.SaveChangesAsync();

            return Ok("added faculty");
        }

        [HttpGet("get-all-faculties")]
        public async Task<IActionResult> GetAllfaculties()
        {
            var faculties = await context.Faculity.ToListAsync();
            var facultiesResult= faculties.Select(faculty => _mapper.Map<FacultyResultDto>(faculty)).ToList();
            return Ok(facultiesResult);
        }

        [HttpGet("get-faculty-by-id")]
        public async Task<IActionResult> getfaculty(string fId)
        {
            var faculty = context.Faculity.FirstOrDefault(f => f.Id == fId);
            if (faculty == null) return BadRequest("No found Facluty");
            var facultyDto = _mapper.Map<FacultyResultDto>(faculty);
            return Ok(facultyDto);
        }

    }
}
