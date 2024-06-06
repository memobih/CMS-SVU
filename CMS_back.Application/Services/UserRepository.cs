using CMS_back.Consts;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.IGenericRepository;
using CMS_back.Interfaces;
using CMS_back.Models;
using Microsoft.AspNetCore.Identity;
using CMS_back.GenericRepository;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using CMS_back.Application.Interfaces;
using CMS_back.Application.Helpers;

namespace CMS_back.Services
{
    public class UserRepository : IUserRepository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGenericRepository<Faculity> _facultyRepo;
        private readonly IGenericRepository<ControlUsers> _controlUsersRepo;
        private readonly IMapper _mapper;
        private readonly IUserHelpers _userHelpers;

        public UserRepository(UserManager<ApplicationUser> userManager, IGenericRepository<Faculity> facultyRepo
            , IHttpContextAccessor contextAccessor, IMapper mapper, IUserHelpers userHelpers, IGenericRepository<ControlUsers> controlUsersRepo)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _facultyRepo = facultyRepo;
            _controlUsersRepo = controlUsersRepo;
            _mapper = mapper;
            _userHelpers = userHelpers;
        }
        public async Task<IdentityResult> AddAsync(ApplicationUser user, string password)
        {
            IdentityResult result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, user.Type);
            }
            return result;
        }

        public async Task<ApplicationUser> GetUserByUsernameAndPasswordAsync(LoginUserDto userDto)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(userDto.UserName);
            if (user != null)
            {
                bool found = await _userManager.CheckPasswordAsync(user, userDto.Password);
                if (found)
                {
                    return user;
                }
            }
            return null;
        }

        public async Task<IEnumerable<StaffResultDto>> GetFacultyUsers(string facultyId)
        {
            var faculty = await _facultyRepo.GetById(facultyId, ["Users"]);
            if (faculty == null) throw new Exception("FaculityID Not Found");
            var usersResult = _mapper.Map<IEnumerable<StaffResultDto>>(faculty.Users);
            return usersResult;
        }

        public async Task<List<ContorlAndItsUserJobTitleDTO>>? GetControlUsers(string controlId)
        {
            var controlsUser = await _controlUsersRepo.FindAsync(c => c.ControlID == controlId, ["User", "Control"]);
            if (controlsUser == null) throw new Exception("Control Not Found");
            var resultUsers = _mapper.Map<List<ContorlAndItsUserJobTitleDTO>>(controlsUser);
            return resultUsers;
        }

        public async Task<IUserResult> GetCurrentUser()
        {
            var currentUser = await _userHelpers.GetCurrentUserAsync();
            IUserResult userResult;
            Faculity faculity;
            if (currentUser.Type == ConstsRoles.Staff)
            {
                userResult = _mapper.Map<StaffResultDto>(currentUser);
                faculity = await _facultyRepo.GetById(currentUser.FaculityEmployeeID);
                userResult.FaculityName = faculity.Name;
            }
            else if (currentUser.Type == ConstsRoles.AdminFaculty)
            {
                userResult = _mapper.Map<LeaderResultDto>(currentUser);
                faculity = await _facultyRepo.GetById(currentUser.FaculityLeaderID);
                userResult.FaculityName = faculity.Name;
            }
            else
            {
                userResult = _mapper.Map<AdminUniversityResultDto>(currentUser);
            }
            return userResult;
        }

        public async Task<List<UserWithHisControlDTO>?> GetUserOfControl(string userId)
        {
            var controlUser = await _controlUsersRepo.FindAsync(c => c.UserID == userId, ["User", "Control"]);
            if (controlUser == null) throw new Exception("User Not Found in any Control");
            var resultUser = _mapper.Map<List<UserWithHisControlDTO>>(controlUser);
            return resultUser;
        }

        public async Task<ApplicationUser?> GetHeadOfControl(string controlId)
        {
            var controlHead = await _controlUsersRepo.FindFirstAsync((c => c.ControlID == controlId && c.JobType == JobType.Head), ["User"]);
            if (controlHead == null) return null;
            return controlHead.User;
        }


    }
}
