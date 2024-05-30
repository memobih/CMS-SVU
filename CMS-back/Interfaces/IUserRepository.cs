using CMS_back.DTO;
using CMS_back.Models;
using Microsoft.AspNetCore.Identity;
namespace CMS_back.Interfaces
{
    public interface IUserRepository 
    {
        Task<IdentityResult> AddAsync(ApplicationUser user, string password);
        Task<ApplicationUser> GetUserByUsernameAndPasswordAsync(LoginUserDto user);
        Task<IEnumerable<ApplicationUser>> GetFacultyUsers(string facultyId);
        Task<List<ControlUsers>> GetControlUsers(string controlId);
        Task<ApplicationUser> GetCurrentUser();
        Task<List<ControlUsers>?> GetUserOfControl(string controlId);
        Task<ApplicationUser> GetHeadOfControl(string controlId);

    }
}
