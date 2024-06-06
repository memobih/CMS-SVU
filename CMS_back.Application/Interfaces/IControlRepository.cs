using CMS_back.DTO;

namespace CMS_back.Interfaces
{
    public interface IControlRepository
    {
        Task<ControlResultDto> GetByIdAsync(string id);
        Task<IEnumerable<ControlResultDto>> GetAllAsync();
        Task<bool> AddAsync(ControlDTO entity, string Fid);
        Task<bool> UpdateAsync(ControlDTO entity ,string id);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<ControlResultDto>> GetControlsByFaculityIdAsync(string FaculityId);     
        Task<IEnumerable<ControlResultDto>> GetControlsByAcadYearAsync(string AcadYear);
    }

}
