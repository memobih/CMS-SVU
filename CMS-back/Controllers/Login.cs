﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using CMS_back.Reposatory.Models;
using CMS_back.DTO;
using CMS_back.Data;
using System.Security.Principal;

namespace CMS_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {

        private readonly UserManager<ApplicationUser> usermanger;
        private readonly IConfiguration config;
        private readonly CMSContext context;


        public Login(UserManager<ApplicationUser> usermanger, IConfiguration config,CMSContext _context)
        {
            this.usermanger = usermanger;
            this.config = config;
            context = _context;
        }


        [HttpPost]
        public async Task<IActionResult> signin(LoginUserDto userDto)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser? user = await usermanger.FindByNameAsync(userDto.UserName);
                if (user != null)
                {
                    bool found = await usermanger.CheckPasswordAsync(user, userDto.Password);
                    if (found)
                    {
                        //Claims Token
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        //get role
                        var roles = await usermanger.GetRolesAsync(user);
                        foreach (var itemRole in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, itemRole));
                        }
                        SecurityKey securityKey =
                           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));

                        SigningCredentials signincred =
                                    new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        //Create token
                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: config["JWT:ValidIssuer"],//url web api
                                                              //audience: config["JWT:ValidAudiance"],//url consumer angular
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signincred
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = mytoken.ValidTo
                        });
                    }
                }
                return Unauthorized();
            }
            return Unauthorized();
        }
    }
}
