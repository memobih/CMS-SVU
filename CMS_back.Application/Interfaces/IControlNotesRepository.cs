using CMS_back.DTO;

namespace CMS_back.Interfaces
{
    public interface IControlNotesRepository
    {
        Task<bool> AddAsync(controlNoteDTO entity, string Cid);
        Task<IEnumerable<ControlNotesResultDTO>> GetAllAsync(string id);
        Task<IEnumerable<ControlNotesResultDTO>> GetNotesToHeadControl(string id);
        Task<IEnumerable<ControlNotesResultDTO>> GetNotesToHeadFaculty(string id);
        Task<IEnumerable<ControlNotesResultDTO>> GetNotesToHeadUnivarsity(string id);
    }
}
