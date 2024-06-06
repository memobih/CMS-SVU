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
        private readonly CMSContext _context;
        private readonly IMapper Mapper;

        public ControlNotesController(CMSContext context
            , IMapper mapper, IControlNotesRepository controlNotesRepository)
        {
            _controlNotesRepository = controlNotesRepository;
            _context = context;
            Mapper = mapper;
        }

        [HttpPost("createcontrolnote/{Cid}")]
        public async Task<IActionResult> create(controlNoteDTO controlNoteDTO, string Cid)
        {
            var controlNote = await _controlNotesRepository.AddAsync(controlNoteDTO, Cid);
            return controlNote ? Ok("Note Created Successfully") : BadRequest("Invalid Note Data");
        }

        [HttpGet("getallcontrolnotes/{Cid}")]
        public async Task<IActionResult> get(string Cid)
        {
            var control_notes = await _controlNotesRepository.GetAllAsync(Cid);
            return Ok(control_notes);
        }

        [HttpGet("notetoheadcontrol/{Cid}")]

        public async Task<IActionResult> getNotesToHeadControl([FromRoute] string Cid)
        {
            var control_notes = await _controlNotesRepository.GetNotesToHeadControl(Cid);
            return Ok(control_notes);
        }

        [HttpGet("notetoheadfaculty/{Cid}")]
        public async Task<IActionResult> getNotesTOHeadFaculty([FromRoute] string Cid)
        {
            var control_notes = await _controlNotesRepository.GetNotesToHeadFaculty(Cid);
            return Ok(control_notes);
        }

        [HttpGet("notetoheadunivarsity/{Cid}")]
        public async Task<IActionResult> getNotesTOHeadUnivarsity([FromRoute] string Cid)
        {
            var control_notes = await _controlNotesRepository.GetNotesToHeadUnivarsity(Cid);
            return Ok(control_notes);
        }

    }
}
