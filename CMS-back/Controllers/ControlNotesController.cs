using AutoMapper;
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
    public class ControlNotesController : ControllerBase
    {
        private readonly IControlNotesRepository _controlNotesRepository;

        public ControlNotesController(IControlNotesRepository controlNotesRepository)
        {
            _controlNotesRepository = controlNotesRepository;
        }

        [HttpPost("createcontrolnote/{cid}")]
        public async Task<IActionResult> create(controlNoteDTO controlNoteDTO, string cid)
        {
            var controlNote = await _controlNotesRepository.AddAsync(controlNoteDTO, cid);
            return controlNote ? Ok("Note Created Successfully") : BadRequest("Invalid Note Data");
        }

        [HttpGet("getallcontrolnotes/{cid}")]
        public async Task<IActionResult> get(string cid)
        {
            var control_notes = await _controlNotesRepository.GetAllAsync(cid);
            return Ok(control_notes);
        }

        [HttpGet("notetoheadcontrol/{cid}")]
        public async Task<IActionResult> getNotesToHeadControl([FromRoute] string cid)
        {
            var control_notes = await _controlNotesRepository.GetNotesToHeadControl(cid);
            return Ok(control_notes);
        }

        [HttpGet("notetoheadfaculty/{cid}")]
        public async Task<IActionResult> getNotesTOHeadFaculty([FromRoute] string cid)
        {
            var control_notes = await _controlNotesRepository.GetNotesToAdminFaculity(cid);
            return Ok(control_notes);
        }

        [HttpGet("notetoheadunivarsity/{cid}")]
        public async Task<IActionResult> getNotesTOHeadUniversity([FromRoute] string cid)
        {
            var control_notes = await _controlNotesRepository.GetNotesToAdminUniversity(cid);
            return Ok(control_notes);
        }

    }
}
