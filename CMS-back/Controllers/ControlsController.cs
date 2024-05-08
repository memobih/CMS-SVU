using Microsoft.AspNetCore.Mvc;
using CMS_back.DTO;
using CMS_back.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CMS_back.Models;
using AutoMapper;
using System;
using CMS_back.Consts;
using CMS_back.Mailing;


namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ControlsController : ControllerBase
    {
        public CMSContext context { get; }
        public IConfiguration cfg { get; }
        public UserManager<ApplicationUser> Usermanager { get; }
        public IHttpContextAccessor ContextAccessor { get; }
        public IMailingService MailingService { get; }

        private readonly IMapper _mapper;
        public ControlsController(CMSContext _context,IConfiguration _cfg, 
            UserManager<ApplicationUser> usermanager, IHttpContextAccessor contextAccessor,IMapper mapper, IMailingService mailingService)
        {
            context=_context;
            cfg=_cfg;
            Usermanager=usermanager;
            ContextAccessor=contextAccessor;
            _mapper = mapper;
            MailingService=mailingService;
        }

        //[HttpPost("create/{Fid}")]
        //[Authorize(Roles = ConstsRoles.AdminFaculty)]
        //public async Task<IActionResult> createControl(ControlDTO controldto, string Fid)
        //{ 
        //    Faculity? facultiy = context.Faculity.FirstOrDefault(f => f.Id == Fid);
        //    if (facultiy != null)
        //    {
        //        Control control = new Control();
        //        control.Name = controldto.Name;
        //        control.Faculity_Node = controldto.Faculity_Node;
        //        control.Faculity_Semester = controldto.Faculity_Semester;
        //        control.Faculity_Phase = controldto.Faculity_Phase;
        //        control.Start_Date = controldto.Start_Date;
        //        control.End_Date = controldto.End_Date;
        //        control.ACAD_YEAR = controldto.ACAD_YEAR;

        //        //control.Faculity = facultiy;
        //        control.FaculityID = Fid;

        //        ApplicationUser? mamager = context.Users.FirstOrDefault(u => u.Id == controldto.ControlManagerID);
        //        if (mamager == null) return BadRequest("Invalid Head of control id");
        //        ControlUsers userControl = new ControlUsers()
        //        {
        //            ControlID = control.Id,
        //            Control = control,
        //            UserID = mamager.Id,
        //            User = mamager,
        //            JobType = JobType.Head
        //        };
        //        if (mamager.Email != null)
        //        {
        //            var message = new Mailing.MailMessage(new string[] { mamager.Email }, "Control System", "You are Head of new Control");
        //            MailingService.SendMail(message);
        //        }
        //        context.ControlUsers.Add(userControl);

        //        var creator = ContextAccessor.HttpContext.User;
        //        if (creator == null) return BadRequest("Creator ID invalid");
        //        var userCreater = await Usermanager.GetUserAsync(creator);
        //        control.UserCreator = userCreater;
        //        control.UserCreatorID = userCreater.Id;

        //        var usersIDs = controldto.ContorlUsersIDs;
        //        foreach (var id in usersIDs)
        //        {
        //            ApplicationUser? user = context.Users.FirstOrDefault(u => u.Id == id);
        //            if (user == null) return BadRequest("invalid member id");
        //            ControlUsers memberControl = new ControlUsers()
        //            {
        //                ControlID = control.Id,
        //                Control = control,
        //                UserID = user.Id,
        //                User = user,
        //                JobType = JobType.Member
        //            };
        //            if (user.Email != null)
        //            {
        //                var message = new Mailing.MailMessage(new string[] { user.Email }, "Control System", "You are Member in new Control");
        //                MailingService.SendMail(message);
        //            }
        //            context.ControlUsers.Add(memberControl);
        //        }

        //        var subjectIDs = controldto.ControlSubjectsIDs;
        //        foreach (var id in subjectIDs)
        //        {
        //            Subject? subject = context.Subject.FirstOrDefault(u => u.Id == id);
        //            if (subject == null) return BadRequest("invalid subjet id");
        //            ControlSubject cs = new ControlSubject();
        //            cs.Subject = subject;
        //            cs.SubjectID = subject.Id;
        //            cs.Control = control;
        //            cs.ControlID = control.Id;
        //            //control.ControlSubjects.Add(cs);
        //            context.ControlSubject.Add(cs);
        //        }

        //        context.Control.Add(control);
        //        facultiy.Controls.Add(control);

        //       if(await context.SaveChangesAsync() > 0)
        //            return Ok("Created Control");
        //       return BadRequest("Failed to add data");
        //    }
        //    return BadRequest("Faculty not found");
        //}

        

            [HttpPut("edit")]
        [Authorize(Roles = ConstsRoles.AdminFaculty)]
        public async Task<IActionResult> EditControl(ControlDTO controldto, string Cid)
        {
            var creator = ContextAccessor.HttpContext.User;
            var userCreator = await Usermanager.GetUserAsync(creator);
            if (userCreator == null) return BadRequest("Creator ID invalid");

            Control? control = context.Control.FirstOrDefault(c => c.Id == Cid);
            if (control == null) return BadRequest("Invalid control id");

            if (userCreator.Id != control.UserCreatorID) return BadRequest("Invalid creator id");
            _mapper.Map(controldto, control);
            var subjects=new List<ControlSubject>();
            foreach(var id in controldto.SubjectsIds)
            {
                var subject=context.ControlSubject.FirstOrDefault(c => c.ControlID==control.Id);
                subjects.Add(subject);
            }

            var users = new List<ControlUsers>();
            foreach (var id in controldto.UsersIds)
            {
                var user = context.ControlUsers.FirstOrDefault(c => c.ControlID == control.Id);
                users.Add(user);
            }
            control.ControlSubjects = subjects;
            control.ControlUsers = users;
            //context.ControlSubject.RemoveRange(subjects);
            //context.ControlUsers.RemoveRange(users);
            context.Control.Update(control);
            await context.SaveChangesAsync();
            return Ok("Updated Control");
        }
        //Faculity? faculity = context.Faculity.FirstOrDefault(f => f.Id == control.FaculityID);
        //if (faculity == null) return BadRequest("Control does not have a faculty");
        //var controlUser=control.ControlUsers.FirstOrDefault(c => c.ControlID == control.Id&&c.JobType==JobType.Head);

        //control.Name = controldto.Name;
        //control.Faculity_Node = controldto.Faculity_Node;
        //control.Faculity_Semester = controldto.Faculity_Semester;
        //control.Faculity_Phase = controldto.Faculity_Phase;
        //control.Start_Date = controldto.Start_Date;
        //control.End_Date = controldto.End_Date;
        //control.ACAD_YEAR = controldto.ACAD_YEAR;


        //controlUser.UserID = controldto.ControlManagerID;
        // Update Control Manager
        //ControlUsers controlUsers = context.ControlUsers.FirstOrDefault(c => c.ControlID == control.Id);
        //if (controlUsers == null) return BadRequest("Invalid control");
        //ApplicationUser? manager = context.Users.FirstOrDefault(u => u.Id == controldto.ControlManagerID);
        //if (manager == null) return BadRequest("Invalid Head of control id");
        //controlUsers.UserID = manager.Id;

        // Update Control Members
        //var usersIDs = controldto.ContorlUsersIDs;
        //foreach (var id in usersIDs)
        //{
        //    ApplicationUser? user = context.Users.FirstOrDefault(u => u.Id == id);
        //    if (user == null) return BadRequest("Invalid member id");
        //    ControlUsers memberControl = new ControlUsers()
        //    {
        //        ControlID = control.Id,
        //        UserID = user.Id,
        //        JobType = JobType.Member
        //    };
        //    context.ControlUsers.Add(memberControl);
        //}

        // Update Control Subjects
        //var subjectIDs = controldto.ControlSubjectsIDs;
        //foreach (var id in subjectIDs)
        //{
        //    Subject? subject = context.Subject.FirstOrDefault(u => u.Id == id);
        //    if (subject == null) return BadRequest("Invalid subject id");
        //    ControlSubject cs = new ControlSubject();
        //    cs.SubjectID = subject.Id;
        //    cs.ControlID = control.Id;
        //    context.ControlSubject.Add(cs);
        //}

        // Save changes to the database
        [HttpGet("allControllers")]
        public async Task<IActionResult> index()
        {
            var controlles = await context.Control.Include(c=>c.ControlSubjects).ToListAsync();
            if (controlles == null) return Ok(new List<Control>());
            var controlsResultDto=controlles.Select(control=>_mapper.Map<ControlResultDto>(control)).ToList();
            return Ok(controlsResultDto);
        }
        
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> detail(string id)
        {
            var control = await context.Control.Include(c=>c.ControlSubjects).Include(c => c.ControlUsers).SingleOrDefaultAsync(x => x.Id == id);
            if (control == null) return BadRequest("Not Found");
            var controlResult=_mapper.Map<ControlResultDto>(control);
            return Ok(controlResult);
        }
        
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = ConstsRoles.AdminFaculty)]
        public async Task<IActionResult> delete(string id)
        {
            var controle = await context.Control.SingleOrDefaultAsync(x => x.Id == id);
            if (controle == null) return BadRequest("Not Found");
            else
            {
                context.Control.Remove(controle);
                context.SaveChanges();
                return Ok("Deleted");
            }
        }

        [HttpGet("{Fid}")]
        public async Task<IActionResult> get (string Fid)
        {
            var faculty = context.Faculity.Include(f=>f.Controls).ThenInclude(c=> c.ControlSubjects).FirstOrDefault(x => x.Id == Fid);
            if (faculty == null) return BadRequest("Faculty not found");
            if (faculty.Controls == null) return BadRequest("Faculty not has controls");
            var controlsResultDto=faculty.Controls.Select(control => _mapper.Map<ControlResultDto>(control)).ToList();
            return Ok(controlsResultDto);
        }

        [HttpGet("acadmec-year")]
        public async Task<IActionResult> GetControlAcademcYear(string AY)
        {
            var control = context.Control.Where(c => c.ACAD_YEAR == AY);
            if (control == null) return BadRequest("Control not found");
            var controlsResultDto = control.Select(control => _mapper.Map<ControlResultDto>(control)).ToList();
            return Ok(controlsResultDto);
        }
    }
}
