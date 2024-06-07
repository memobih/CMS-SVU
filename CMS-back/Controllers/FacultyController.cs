using AutoMapper;
using CMS_back.Consts;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class FacultyController : ControllerBase
    {
        private readonly IFaculityRepository _faculityRepository;
        public FacultyController(IFaculityRepository faculityRepository)
        {
            _faculityRepository = faculityRepository;
        }

        [HttpPost("add-faculity")]
        [Authorize(Roles = ConstsRoles.AdminUniversity)]
        public async Task<IActionResult> create(FacultyDTO facultyDTO)
        {
            var faculity = await _faculityRepository.AddAsync(facultyDTO);
            return faculity ? Ok("Faculity Added Successfully") : BadRequest("Invalid Faculity Data");
        }

        [HttpGet("get-faculity-by-id/{fid}")]
        public async Task<IActionResult> getfacuilty(string fid)
        {
            var faculity = await _faculityRepository.GetByIdAsync(fid);
            return Ok(faculity);
        }

        [HttpGet("faculitynode/{fid}")]
        public async Task<IActionResult> getfacultynode([FromRoute] string fid)
        {
            var facultyNode = await _faculityRepository.GetFaculityNode(fid);
            return Ok(facultyNode);
        }   

        [HttpGet("get-all-faculities")]
        public async Task<IActionResult> GetAllfaculities()
        {
            var faculities = await _faculityRepository.GetAllAsync();
            return Ok(faculities);
        }

        [HttpGet("getallacadyears")]
        public async Task<IActionResult> getAllAcadYears()
        {
            var acadYears = await _faculityRepository.GetAllAcadYearAsync();
            return Ok(acadYears);
        }
    }
}
