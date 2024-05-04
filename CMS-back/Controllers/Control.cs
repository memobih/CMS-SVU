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
    [Route("api/[controller]")]
    [ApiController]
    public class Control : ControllerBase
    {
    }
}
