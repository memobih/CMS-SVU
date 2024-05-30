using CMS_back.Models;

namespace CMS_back.Interfaces
{
    public interface ISubjectRepository
    {
        Task<ICollection<Subject>> GetFacultySubject(string facultyId);
        Task<IEnumerable<Subject>> GetControlSubject(string controlId);
        Task<Subject> AddSubject(Subject subject);
        Task<Subject> FinishedSubject(string subjectId);
        Task<Subject> ReviewSubject(string subjectId);
    }
}
