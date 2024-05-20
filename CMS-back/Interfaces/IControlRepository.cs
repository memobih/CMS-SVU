using CMS_back.DTO;
using CMS_back.Models;
using System.Linq.Expressions;

namespace CMS_back.Interfaces
{
    public interface IControlRepository
    {

        Task<ControlResultDto> GetByIdAsync(string id);
        Task<IEnumerable<ControlResultDto>> GetAllAsync();
        Task AddAsync(Control entity);
        Task UpdateAsync(ControlDTO entity ,string id);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<ControlResultDto>> GetControlsByAcadYearAsync(string AcadYear);
        Task<IEnumerable<ControlResultDto>> GetControlsByFaculityIdAsync(string FaculityId);
    }

}
