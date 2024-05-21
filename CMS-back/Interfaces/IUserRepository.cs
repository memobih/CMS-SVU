using CMS_back.DTO;
using CMS_back.Models;

namespace CMS_back.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> AddAsync(ApplicationUser user, string password);
        Task<ApplicationUser> GetUserByUsernameAndPasswordAsync(LoginUserDto user);
        Task<IEnumerable<ApplicationUser>> GetFacultyUsers(string facultyId);
        Task<List<ApplicationUser>> GetControlUsers(string controlId);
        Task<ApplicationUser> GetCurrentUser();
        Task<IEnumerable<ControlUsers>>? GetUserOfControl(string controlId);
        Task<ApplicationUser> GetHeadOfControl(string controlId);

    }
}
