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
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;


namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class Controls : ControllerBase
    {
        public CMSContext context { get; }
        public IConfiguration cfg { get; }

        public Controls(CMSContext _context,IConfiguration _cfg)
        {
            context=_context;
            cfg=_cfg;
        }

        [HttpPost("create")]
        public async Task<IActionResult> createControl(ControlDTO controldto, string Fid)
        {
            
            Faculity? facultiy = context.Faculities.FirstOrDefault(f => f.ID == Fid);
            if (facultiy != null)
            {
                Control control = new Control();
                control.Name = controldto.Name;
                control.Faculity_Node = controldto.Faculity_Node;
                control.Faculity_Semester = controldto.Faculity_Semester;
                control.Faculity_Phase = controldto.Faculity_Phase;
                control.Start_Date = controldto.Start_Date;
                control.End_Date = controldto.End_Date;
                control.ACAD_YEAR = controldto.ACAD_YEAR;

                control.Faculity = facultiy;
                control.FaculityID = Fid;

                control.ControlManagerID = control.ControlManagerID;
                ApplicationUser? mamager = context.Users.FirstOrDefault(u => u.Id == controldto.ControlManagerID);
                if (mamager == null) return BadRequest("Invalid Head of control id");
                control.ControlManager = mamager;

                control.UserCreatorID = controldto.ControlCreateID;
                ApplicationUser? creator = context.Users.FirstOrDefault(u => u.Id == controldto.ControlCreateID);
                if (creator == null) return BadRequest("Invalid creator of control id");
                control.UserCreator = creator;

                var usersIDs = controldto.ContorlUsersIDs;
                foreach (var id in usersIDs)
                {
                    ApplicationUser? user = context.Users.FirstOrDefault(u => u.Id == id);
                    if (user == null) return BadRequest("invalid member id");
                    control.Users.Add(user);
                }

                var subjectIDs = controldto.ControlSubjectsIDs;
                foreach (var id in subjectIDs)
                {
                    Subject? subject = context.Subjects.FirstOrDefault(u => u.ID == id);
                    if (subject == null) return BadRequest("invalid subjet id");
                    ControlSubject cs = new ControlSubject();
                    cs.Subject = subject;
                    cs.SubjectID = subject.ID;
                    cs.Control = control;
                    cs.ControlID = control.ID;
                    control.ControlSubjects.Add(cs);
                }

                context.Controls.Add(control);
                facultiy.Controls.Add(control);

                await context.SaveChangesAsync();

                return Ok("Created Control");
            }
            return BadRequest("Faculty not found");
        }

        [HttpPost("edit/{Cid:alpha}")]
        public async Task<IActionResult> EditControl(ControlDTO controldto, string Cid)
        {
            Control? control = context.Controls.FirstOrDefault(c => c.ID == Cid);
            if (control == null) return BadRequest("inValid control id");

            Faculity? faculity = context.Faculities.FirstOrDefault(f => f.ID == control.FaculityID);
            if (faculity == null) return BadRequest("Control not has faculty");
            faculity.Controls.Remove(control);

            control.Name = controldto.Name;
            control.Faculity_Node = controldto.Faculity_Node;
            control.Faculity_Semester = controldto.Faculity_Semester;
            control.Faculity_Phase = controldto.Faculity_Phase;
            control.Start_Date = controldto.Start_Date;
            control.End_Date = controldto.End_Date;
            control.ACAD_YEAR = controldto.ACAD_YEAR;

            control.ControlManagerID = control.ControlManagerID;
            ApplicationUser? mamager = context.Users.FirstOrDefault(u => u.Id == controldto.ControlManagerID);
            if (mamager == null) return BadRequest("Invalid Head of control id");
            control.ControlManager = mamager;

            control.UserCreatorID = controldto.ControlCreateID;
            ApplicationUser? creator = context.Users.FirstOrDefault(u => u.Id == controldto.ControlCreateID);
            if (creator == null) return BadRequest("Invalid creator of control id");
            control.UserCreator = creator;

            var usersIDs = controldto.ContorlUsersIDs;
            foreach (var id in usersIDs)
            {
                ApplicationUser? user = context.Users.FirstOrDefault(u => u.Id == id);
                if (user == null) return BadRequest("invalid member id");
                control.Users.Add(user);
            }

            var subjectIDs = controldto.ControlSubjectsIDs;
            foreach (var id in subjectIDs)
            {
                Subject? subject = context.Subjects.FirstOrDefault(u => u.ID == id);
                if (subject == null) return BadRequest("invalid subjet id");
                ControlSubject cs = new ControlSubject();
                cs.Subject = subject;
                cs.SubjectID = subject.ID;
                cs.Control = control;
                cs.ControlID = control.ID;
                control.ControlSubjects.Add(cs);
            }

            context.Controls.Add(control);
            faculity.Controls.Add(control);
            

            await context.SaveChangesAsync();


            return Ok("Created Control");
        }
        [HttpGet("allControllers")]
        public async Task<IActionResult> index()
        {
            var controlles = await context.Controls.ToListAsync();
            if (controlles == null) return BadRequest();
            return Ok(controlles);
        }
        [HttpGet("detail/{id:alpha}")]
        public async Task<IActionResult> detail(string id)
        {
            var controle = await context.Controls.SingleOrDefaultAsync(x => x.ID == id);
            if (controle == null) return BadRequest();
            else return Ok(controle);
        }
        [HttpGet("delete/{id:alpha}")]
        public async Task<IActionResult> delete(string id)
        {
            var controle = await context.Controls.SingleOrDefaultAsync(x => x.ID == id);
            if (controle == null) return BadRequest();
            else
            {
                context.Remove(controle);
                context.SaveChanges();
                return Ok();
            }
        }
    }
}
}
