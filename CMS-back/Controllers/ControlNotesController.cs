using AutoMapper;
using CMS_back.Consts;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ControlNotesController : ControllerBase
    {

        public CMSContext Context { get; }
        public IHttpContextAccessor ContextAccessor { get; }
        public UserManager<ApplicationUser> Usermanager { get; }
        public IMapper Mapper { get; }

        public ControlNotesController(CMSContext _context, IHttpContextAccessor contextAccessor,
            UserManager<ApplicationUser> usermanager, IMapper mapper)
        {
            Context=_context;
            ContextAccessor=contextAccessor;
            Usermanager=usermanager;
            Mapper=mapper;
        }

        [HttpPost]
        public async Task<IActionResult> create(controlNoteDTO controlNoteDTO, string Cid)
        {
            var creator = ContextAccessor.HttpContext.User;
            var userCreater = await Usermanager.GetUserAsync(creator);
            Control_Note control_Note = Mapper.Map<Control_Note>(controlNoteDTO);
            control_Note.WriteDate = DateTime.Now;
            control_Note.WriteBy = userCreater;
            control_Note.Control = Context.Control.FirstOrDefault(c => c.Id == Cid);
            Context.Control_Note.Add(control_Note);
            await Context.SaveChangesAsync();
            return Ok("Notes Created");
        }

        [HttpGet("control/{Cid}")]
        public IActionResult get(string Cid)
        {
            var control_notes = Context.Control_Note.Include(c => c.WriteBy).Where(c => c.ControlID == Cid).ToList();
            var control_notes_result = Mapper.Map<List<ControlNotesResultDTO>>(control_notes);
            return Ok(control_notes_result);
        }

        [HttpGet("notetoheadcontrol/{Cid}")]
        public IActionResult getNotesTOHeadControl([FromRoute] string Cid)
        {
            var allControlMember = Context.Control_Note.Include(c => c.WriteBy).Where(c => c.ControlID == Cid).ToList();
            List<ControlNotesResultDTO>? controlNotesResultDTOs = new List<ControlNotesResultDTO>();
            foreach (var users in allControlMember)
            {
                var member = Context.ControlUsers.FirstOrDefault(c => c.UserID == users.WriteByID);
                if (member == null || member.JobType != JobType.Member) continue;
                controlNotesResultDTOs.Add(new ControlNotesResultDTO()
                {
                    Description = users.Description,
                    WriteDate = users.WriteDate,
                    WriteBy = users.WriteBy,
                });
            }
            return Ok(controlNotesResultDTOs);
        }

        [HttpGet("notetoheadfaculty/{Cid}")]
        public IActionResult getNotesTOHeadFaculty([FromRoute] string Cid)
        {
            var allControlMember = Context.Control_Note.Include(c => c.WriteBy).Where(c => c.ControlID == Cid).ToList();
            List<ControlNotesResultDTO>? controlNotesResultDTOs = new List<ControlNotesResultDTO>();
            foreach (var users in allControlMember)
            {
                var member = Context.ControlUsers.FirstOrDefault(c => c.UserID == users.WriteByID);
                if (member == null || member.JobType != JobType.Head) continue;
                controlNotesResultDTOs.Add(new ControlNotesResultDTO()
                {
                    Description = users.Description,
                    WriteDate = users.WriteDate,
                    WriteBy = users.WriteBy,
                });
            }
            return Ok(controlNotesResultDTOs);
        }

    }
}
