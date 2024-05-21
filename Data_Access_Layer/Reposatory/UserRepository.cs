using Data_Access_Layer.Consts;
using Data_Access_Layer.Data;
using Data_Access_Layer.IGenericRepository;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Data_Access_Layer.GenericRepository;

namespace Data_Access_Layer.Services
{
    public class UserRepository : IUserRepository
    {
        public CMSContext context { get; }
        public UserManager<ApplicationUser> userManager { get; }
        public IHttpContextAccessor contextAccessor { get; }
        public IGenericRepository<Faculity> _facultyRepository { get; }
        public IGenericRepository<ControlUsers> _controlUsersRepository { get; }

        public UserRepository(CMSContext _context, UserManager<ApplicationUser> _userManager, IHttpContextAccessor _contextAccessor)
        {
            context=_context;
            userManager=_userManager;
            contextAccessor=_contextAccessor;
            _facultyRepository = new GenericRepository<Faculity>(_context);
            _controlUsersRepository= new GenericRepository<ControlUsers>(_context);
        }
        public async Task<ApplicationUser> AddAsync(ApplicationUser user,string password)
        {
            IdentityResult result = await userManager.CreateAsync(user, password);
            if(result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, user.Type);
                return user;
            }
            return null;
        }
        public async Task<ApplicationUser> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            ApplicationUser? user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                bool found = await userManager.CheckPasswordAsync(user, password);
                if (found)
                {
                    return user;
                }
            }
            return null;
        }
        public async Task<IEnumerable<ApplicationUser>> GetFacultyUsers(string facultyId)
        {
            var faculty = await _facultyRepository.GetById(facultyId, ["Users"]);
            if (faculty == null) return null;
            return faculty.Users;
        }
        public async Task<List<ApplicationUser>>? GetControlUsers(string controlId)
        {
            var controlsUser = await _controlUsersRepository.FindAsync(c => c.ControlID == controlId, ["User"]);
            if (controlsUser == null) return null;
            return controlsUser.Select(c => c.User).ToList();
        }
        public async Task<ApplicationUser> GetCurrentUser()
        {
            var user = contextAccessor.HttpContext.User;
            if (user == null) return null;
            var currentUser = await userManager.GetUserAsync(user);
            return currentUser;

        }
        public async Task<IEnumerable<ControlUsers>>? GetUserOfControl(string userId)
        {
            var control = await _controlUsersRepository.FindAsync(c => c.UserID == userId, ["User","Control"]);
            if (control == null) return null;
            return control.ToList();
        }
        public async Task<ApplicationUser?> GetHeadOfControl(string controlId)
        {
            var controlHead = await _controlUsersRepository.FindFirstAsync((c => c.ControlID == controlId && c.JobType == JobType.Head),
                ["User"]);
            if (controlHead == null) return null;
            return controlHead.User;
        }
    }
}
