using CMS_back.DTO;

namespace CMS_back.Interfaces
{
    public interface IFaculityRepository
    {
        Task<FacultyResultDto> GetByIdAsync(string id);
        Task<IEnumerable<FacultyResultDto>> GetAllAsync();
        Task<bool> AddAsync(FacultyDTO entity);
        Task<List<FacultyNodeDTO>> GetFaculityNode(string FacultyNodeId);


    }
}
