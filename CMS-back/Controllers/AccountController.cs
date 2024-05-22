using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CMS_back.DTO;
using CMS_back.Data;
using CMS_back.Models;
using CMS_back.Consts;
using System.Net.Mail;
using CMS_back.Mailing;
using Microsoft.AspNetCore.Authorization;
using CMS_back.Services;
using AutoMapper;


namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly IMailingService _mailingService;
        private readonly UserRepository _repoUser;
        public CMSContext context { get; }
        public UserManager<ApplicationUser> Usermanager { get; }
        public IHttpContextAccessor ContextAccessor { get; }
        public IMapper Mapper { get; }

        public AccountController(UserManager<ApplicationUser> usermanger, IConfiguration config, CMSContext _context
            , IMailingService mailingService, IHttpContextAccessor contextAccessor,IMapper mapper)
        {
            this.config = config;
            context=_context;
            _mailingService = mailingService;
            Usermanager=usermanger;
            ContextAccessor=contextAccessor;
            Mapper=mapper;
            _repoUser = new UserRepository(_context,usermanger,contextAccessor);
        }

        [HttpPost("login")]
        public async Task<IActionResult> signin(LoginUserDto userDto)
        {
            if (ModelState.IsValid == true)
            {
                var user = await _repoUser.GetUserByUsernameAndPasswordAsync(userDto); 
                if (user != null)
                {
                        //Claims Token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(ClaimTypes.Role, user.Type));

                        if (user.FaculityLeaderID != null)
                            claims.Add(new Claim(ClaimTypes.Sid, user.FaculityLeaderID));

                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        SecurityKey securityKey =
                           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));

                        SigningCredentials signincred =
                                    new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        //Create token
                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: config["JWT:ValidIssuer"],//url web api
                            audience: config["JWT:ValidAudience"],//url consumer angular
                            claims: claims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: signincred
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = mytoken.ValidTo,
                            roles = user.Type
                        });
                }
                return BadRequest("user not found");
            }
            return BadRequest("check to complete all fields");


        }

        [HttpPost("register")]//account/register
        public async Task<IActionResult> Registration(RegisterUserDto userDto)
        {
            if (ModelState.IsValid == true)
            {
                var currentUser = await _repoUser.GetCurrentUser();
                //save
                ApplicationUser user = Mapper.Map<ApplicationUser>(userDto);
                user.Type = ConstsRoles.Staff;
                if(currentUser != null)
                    user.FaculityEmployeeID = currentUser.FaculityLeaderID;

                var result = await _repoUser.AddAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    if (user.Email != null && currentUser != null)
                    {
                        var message = new Mailing.MailMessage(new string[] { user.Email }, "Control System", $"User {currentUser.Name} register for you in site.");
                        _mailingService.SendMail(message);
                    }
                    return Ok("User Added");
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);
        }
    }
}
