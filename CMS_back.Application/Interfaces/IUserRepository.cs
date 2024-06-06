using CMS_back.Application.Interfaces;
using CMS_back.DTO;
using CMS_back.Models;
using Microsoft.AspNetCore.Identity;

namespace CMS_back.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> AddAsync(ApplicationUser user, string password);
        Task<ApplicationUser> GetUserByUsernameAndPasswordAsync(LoginUserDto user);
        Task<IEnumerable<StaffResultDto>> GetFacultyUsers(string facultyId);
        Task<List<ContorlAndItsUserJobTitleDTO>> GetControlUsers(string controlId);
        Task<IUserResult> GetCurrentUser();
        Task<List<UserWithHisControlDTO>?> GetUserOfControl(string controlId);
        Task<ApplicationUser> GetHeadOfControl(string controlId);

    }
}
