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

namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> usermanger;
        private readonly IConfiguration config;
        private readonly IMailingService _mailingService;

        public CMSContext context { get; }

        public AccountController(UserManager<ApplicationUser> usermanger, IConfiguration config, CMSContext _context
            , IMailingService mailingService)
        {
            this.usermanger = usermanger;
            this.config = config;
            context=_context;
            _mailingService = mailingService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> signin(LoginUserDto userDto)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = await usermanger.FindByNameAsync(userDto.UserName);
                if (user != null)
                {
                    bool found = await usermanger.CheckPasswordAsync(user, userDto.Password);
                    if (found)
                    {
                        //Claims Token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        
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
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signincred
                            );
                        if(user.Email != null)
                        {
                            var message = new Mailing.MailMessage(new string[] { user.Email }, "login", "انت عملت لوجين يسطا خد بالك");
                                _mailingService.SendMail(message);
                        }
                        
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = mytoken.ValidTo,
                            roles = await usermanger.GetRolesAsync(user)
                        }) ;
                    }
                    return BadRequest("invalid password");
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
                //save
                ApplicationUser user = new ApplicationUser
                {
                    UserName = userDto.UserName,
                    Name = userDto.Name,
                    Email = userDto.Email,
                    ScientificDegree = userDto.ScientificDegree,
                    Type = userDto.Type
                };  
                if(userDto.FaculityID != null)
                {
                    user.FaculityEmployeeID = userDto.FaculityID;
                    user.FaculityEmployee = context.Faculity.FirstOrDefault(f => f.Id == userDto.FaculityID);
                }
                IdentityResult result = await usermanger.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    await usermanger.AddToRoleAsync(user, userDto.Type);
                    
                    return Ok("User Added");
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);
        }
    }
}
