using CMS_back.Application.DTO;
using CMS_back.DTO;
using CMS_back.Models;

namespace CMS_back.Interfaces
{
    public interface IFaculityRepository
    {
        Task<FacultyResultDto> GetByIdAsync(string id);
        Task<IEnumerable<FacultyResultDto>> GetAllAsync();
        Task<bool> AddAsync(FacultyDTO entity);
        Task<List<FacultyNodeDTO>> GetFaculityNode(string FacultyNodeId);
        Task<IEnumerable<AcadYearDTO>> GetAllAcadYearAsync();
    }
}
