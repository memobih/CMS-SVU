using AutoMapper;
using CMS_back.Consts;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.IGenericRepository;
using CMS_back.Interfaces;
using CMS_back.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CMS_back.Services
{
    public class ControlNotesRepository : IControlNotesRepository
    {
        private readonly CMSContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Control_Note> _genericRepository;

        public ControlNotesRepository(CMSContext context, IMapper mapper, UserManager<ApplicationUser> usermanager
            , IGenericRepository<Control_Note> genericRepository, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _genericRepository = genericRepository;
            _contextAccessor = contextAccessor;
            _usermanager = usermanager;

        }
        public async Task<bool> AddAsync(controlNoteDTO controlNoteDto, string Cid)
        {
            var control = _context.Control.FirstOrDefault(c => c.Id == Cid);
            if (control == null) throw new Exception("Invalid Control ID");

            var creator = _contextAccessor.HttpContext.User;
            var userCreater = await _usermanager.GetUserAsync(creator);
            Control_Note control_Note = _mapper.Map<Control_Note>(controlNoteDto);
            control_Note.WriteDate = DateTime.Now;
            control_Note.WriteBy = userCreater;
            control_Note.Control = _context.Control.FirstOrDefault(c => c.Id == Cid);
            _genericRepository.Add(control_Note);

            if (await _context.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<IEnumerable<ControlNotesResultDTO>> GetAllAsync(string Cid)
        {
            var control_notes = await _genericRepository.FindAsync(f => f.ControlID == Cid, "WriteBy");
            var control_notes_result = control_notes.Select(_mapper.Map<ControlNotesResultDTO>).ToList();
            return control_notes_result;
        }

        public async Task<IEnumerable<ControlNotesResultDTO>> GetNotesToHeadControl(string Cid)
        {
            var control_notes = await _genericRepository.FindAsync(f => f.ControlID == Cid, "WriteBy");
            List<ControlNotesResultDTO>? controlNotesResultDTOs = new List<ControlNotesResultDTO>();
            foreach (var note in control_notes)
            {
                var member = _context.ControlUsers.FirstOrDefault(c => c.UserID == note.WriteByID);
                if (member == null || member.JobType != JobType.Head) continue; // Member => انا غيرتهم
                controlNotesResultDTOs.Add(new ControlNotesResultDTO()
                {
                    Description = note.Description,
                    WriteDate = note.WriteDate,
                    WriteBy = _mapper.Map<UserResultDto>(note.WriteBy),
                });
            }
            return controlNotesResultDTOs;
        }

        public async Task<IEnumerable<ControlNotesResultDTO>> GetNotesToHeadFaculty(string Cid)
        {
            var control_notes = await _genericRepository.FindAsync(f => f.ControlID == Cid, "WriteBy");
            List<ControlNotesResultDTO>? controlNotesResultDTOs = new List<ControlNotesResultDTO>();
            foreach (var note in control_notes)
            {
                var member = _context.ControlUsers.FirstOrDefault(c => c.UserID == note.WriteByID);
                if (member == null || member.JobType != JobType.Member) continue; //Head => انا غيرتهم
                controlNotesResultDTOs.Add(new ControlNotesResultDTO()
                {
                    Description = note.Description,
                    WriteDate = note.WriteDate,
                    WriteBy = _mapper.Map<UserResultDto>(note.WriteBy),
                });
            }
            return controlNotesResultDTOs;
        }

        public async Task<IEnumerable<ControlNotesResultDTO>> GetNotesToHeadUnivarsity(string Cid)
        {
            var control =  _context.Control.FirstOrDefault(c => c.Id == Cid);
            var control_notes = await _genericRepository.FindAsync(f => f.WriteByID == control.UserCreatorID, "WriteBy");
            var notesResult = control_notes.Select(note => _mapper.Map<ControlNotesResultDTO>(note)).ToList();
            return notesResult;
        }
    }
}
