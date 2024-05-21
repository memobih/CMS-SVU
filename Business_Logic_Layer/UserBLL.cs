using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Data_Access_Layer.IGenericRepository;
using Data_Access_Layer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business_Logic_Layer
{
    public class UserBLL
    {
        private UserRepository UserDAL { get; }
        public IConfiguration Config { get; }

        public UserBLL(CMSContext _context, UserManager<ApplicationUser> _userManager,
            IHttpContextAccessor _contextAccessor, IConfiguration config) {
            UserDAL = new UserRepository(_context,_userManager,_contextAccessor);
            Config=config;
        }

        public async Task<object?> Login(string username, string password)
        {
            var user = await UserDAL.GetUserByUsernameAndPasswordAsync(username, password);
            if (user == null) return null;
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Role, user.Type));

            if (user.FaculityLeaderID != null)
                claims.Add(new Claim(ClaimTypes.Sid, user.FaculityLeaderID));

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            SecurityKey securityKey =
               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["JWT:Secret"]));

            SigningCredentials signincred =
                        new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create token
            JwtSecurityToken mytoken = new JwtSecurityToken(
                issuer: Config["JWT:ValidIssuer"],//url web api
                audience: Config["JWT:ValidAudience"],//url consumer angular
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signincred
                );
            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                expiration = mytoken.ValidTo,
                roles = user.Type
            };
        }
    }
}
