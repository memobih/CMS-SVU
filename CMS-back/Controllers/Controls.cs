using Microsoft.AspNetCore.Mvc;
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
    [Route("[controller]")]
    [ApiController]
    public class Controls : ControllerBase
    {


        public CMSContext context { get; }
        public Controls(CMSContext _context)
        {
            context=_context;
        }


        [HttpPost("create/{Fid:alpha}")]
        public async Task<IActionResult> createControl(ControlDTO controldto)
        {
            Control control = new Control();
            control.Name = controldto.Name;
            var usersIDs = controldto.ContorlUsersIDs;
            foreach(var id in usersIDs)
            {
                ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == id);
                control.Users.Add(user);
            }
        }
    }
}
