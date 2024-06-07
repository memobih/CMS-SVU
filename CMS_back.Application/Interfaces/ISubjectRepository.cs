using CMS_back.DTO;

namespace CMS_back.Interfaces
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<controlSubjectResultDTO>> GetFacultySubject(string facultyId);
        Task<IEnumerable<controlSubjectResultDTO>> GetControlSubject(string controlId);
        Task<bool> AddSubject(subjectDTO subjectdto);
        Task<bool> FinishedSubject(string controlId, string subjectId);
        Task<bool> ReviewSubject(string controlId, string subjectId);
    }
}
