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
    public class ControlsController : ControllerBase
    {
        public CMSContext context { get; }
        public IConfiguration cfg { get; }

        public ControlsController(CMSContext _context,IConfiguration _cfg)
        {
            context=_context;
            cfg=_cfg;
        }

        [HttpPost("create/{Fid:alpha}")]
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

                ApplicationUser? mamager = context.Users.FirstOrDefault(u => u.Id == controldto.ControlManagerID);
                if (mamager == null) return BadRequest("Invalid Head of control id");
                control.ControlManagerID = mamager.Id;
                control.ControlManager = mamager;

                ApplicationUser? creator = context.Users.FirstOrDefault(u => u.Id == controldto.ControlCreateID);
                if (creator == null) return BadRequest("Invalid creator of control id");
                control.UserCreatorID = creator.Id;
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

        [HttpPut("edit/{Cid:alpha}")]
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

            ApplicationUser? mamager = context.Users.FirstOrDefault(u => u.Id == controldto.ControlManagerID);
            if (mamager == null) return BadRequest("Invalid Head of control id");
            control.ControlManagerID = controldto.ControlManagerID;
            control.ControlManager = mamager;

            ApplicationUser? creator = context.Users.FirstOrDefault(u => u.Id == controldto.ControlCreateID);
            if (creator == null) return BadRequest("Invalid creator of control id");
            control.UserCreatorID = controldto.ControlCreateID;
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
            if (controlles == null) return Ok(new List<Control>());
            return Ok(controlles);
        }
        
        [HttpGet("detail/{id:alpha}")]
        public async Task<IActionResult> detail(string id)
        {
            var controle = await context.Controls.SingleOrDefaultAsync(x => x.ID == id);
            if (controle == null) return BadRequest("Not Found");
            else return Ok(controle);
        }
        
        [HttpDelete("delete/{id:alpha}")]
        public async Task<IActionResult> delete(string id)
        {
            var controle = await context.Controls.SingleOrDefaultAsync(x => x.ID == id);
            if (controle == null) return BadRequest("Not Found");
            else
            {
                context.Remove(controle);
                context.SaveChanges();
                return Ok();
            }
        }

        [HttpGet("{Fid}")]
        public async Task<IActionResult> get (string Fid)
        {
            var faculty = context.Faculities.FirstOrDefault(x => x.ID == Fid);
            if (faculty == null) return BadRequest("Faculty not found");
            if (faculty.Controls == null) return BadRequest("Faculty not has controls");
            return Ok(faculty.Controls);
        }

        [HttpGet("semeter/{Sid}/acadmec/{AY}")]
        public async Task<IActionResult> GetControlBySemesterAndAcademcYear(string Sid,string AY)
        {
            var control = context.Controls.Where(c => c.ACAD_YEAR == AY && c.Faculity_Semester == Sid);
            if (control == null) return BadRequest("Control not found");
            return Ok(control);
        }
    }
}
