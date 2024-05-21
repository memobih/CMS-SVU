using Data_Access_Layer.Entities;

namespace Data_Access_Layer.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> AddAsync(ApplicationUser user, string password);
        Task<ApplicationUser> GetUserByUsernameAndPasswordAsync(string username, string password);
        Task<IEnumerable<ApplicationUser>> GetFacultyUsers(string facultyId);
        Task<List<ApplicationUser>> GetControlUsers(string controlId);
        Task<ApplicationUser> GetCurrentUser();
        Task<IEnumerable<ControlUsers>>? GetUserOfControl(string controlId);
        Task<ApplicationUser> GetHeadOfControl(string controlId);

    }
}
