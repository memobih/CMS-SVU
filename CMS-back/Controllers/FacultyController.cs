﻿using AutoMapper;
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

        [HttpPost("add")]
        [Authorize(Roles = ConstsRoles.AdminUniversity)]
        public async Task<IActionResult> create(FacultyDTO facultyDTO)
        {
            var faculity = await _faculityRepository.AddAsync(facultyDTO);
            return faculity ? Ok("Faculity Added Successfully") : BadRequest("Invalid Faculity Data");
        }

        [HttpGet("get-all-faculities")]
        public async Task<IActionResult> GetAllfaculities()
        {
            var faculities = await _faculityRepository.GetAllAsync();
            return Ok(faculities);
        }

        [HttpGet("get-faculity-by-id")]
        public async Task<IActionResult> getfacuilty(string fId)
        {
            var faculity = await _faculityRepository.GetByIdAsync(fId);
            return Ok(faculity);
        }

        [HttpGet("node/{Fid}")]
        public async Task<IActionResult> getfacultynode([FromRoute] string Fid)
        {
            var facultyNode = await _faculityRepository.GetFaculityNode(Fid);
            return Ok(facultyNode);
        }
    }
}
