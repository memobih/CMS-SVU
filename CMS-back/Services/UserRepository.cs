using AutoMapper;
using CMS_back.Consts;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.IGenericRepository;
using CMS_back.Interfaces;
using CMS_back.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using CMS_back.GenericRepository;

namespace CMS_back.Services
{
    public class UserRepository : IUserRepository
    {
        public CMSContext context { get; }
        public UserManager<ApplicationUser> userManager { get; }
        public IHttpContextAccessor contextAccessor { get; }
        public IGenericRepository<Faculity> _facultyRepository { get; }
        public IGenericRepository<ControlUsers> _controlUsersRepository { get; }
        
        public UserRepository(CMSContext _context, UserManager<ApplicationUser> _userManager,
            IHttpContextAccessor _contextAccessor)
        {
            context=_context;
            userManager=_userManager;
            contextAccessor=_contextAccessor;
            _facultyRepository = new GenericRepository<Faculity>(_context);
            _controlUsersRepository= new GenericRepository<ControlUsers>(_context);
        }
        public async Task<IdentityResult> AddAsync(ApplicationUser user,string password)
        {
            IdentityResult result = await userManager.CreateAsync(user, password);
            if(result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, user.Type);
            }
            return result;
        }
        public async Task<ApplicationUser> GetUserByUsernameAndPasswordAsync(LoginUserDto userDto)
        {
            ApplicationUser? user = await userManager.FindByNameAsync(userDto.UserName);
            if (user != null)
            {
                bool found = await userManager.CheckPasswordAsync(user, userDto.Password);
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
        public async Task<List<ControlUsers>>? GetControlUsers(string controlId)
        {
            var controlsUser = await _controlUsersRepository.FindAsync(c => c.ControlID == controlId, ["User", "Control"]);
            if (controlsUser == null) return null;
            return controlsUser.ToList();
        }
        public async Task<ApplicationUser> GetCurrentUser()
        {
            var user = contextAccessor.HttpContext.User;
            if (user == null) return null;
            var currentUser = await userManager.GetUserAsync(user);
            return currentUser;

        }
        public async Task<List<ControlUsers>?> GetUserOfControl(string userId)
        {
            var controlUser = await _controlUsersRepository.FindAsync(c => c.UserID == userId, ["User", "Control"]);
            if (controlUser == null) return null;
            return controlUser.ToList();
        }
        public async Task<ApplicationUser?> GetHeadOfControl(string controlId)
        {
            var controlHead = await _controlUsersRepository.FindFirstAsync((c => c.ControlID == controlId && c.JobType == JobType.Head),["User"]); 
            if (controlHead == null) return null;
            return controlHead.User;
        }
    }
}
