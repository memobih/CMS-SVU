using Microsoft.AspNetCore.Mvc;
using CMS_back.Reposatory.Models;
using CMS_back.DTO;
using CMS_back.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


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
        public ControlsController(CMSContext _context,IConfiguration _cfg, 
            UserManager<ApplicationUser> usermanager, IHttpContextAccessor contextAccessor)
        {
            context=_context;
            cfg=_cfg;
            Usermanager=usermanager;
            ContextAccessor=contextAccessor;
        }

        [HttpPost("create/{Fid}")]
        public async Task<IActionResult> createControl(ControlDTO controldto, string Fid)
        { 
            Faculity? facultiy = context.Faculity.FirstOrDefault(f => f.Id == Fid);
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
                ControlUsers userControl = new ControlUsers()
                {
                    ControlID = control.Id,
                    Control = control,
                    UserID = mamager.Id,
                    User = mamager,
                    JobType = JobType.Head
                };
                context.ControlUsers.Add(userControl);

                var creator = ContextAccessor.HttpContext.User;
                if (creator == null) return BadRequest("Creator ID invalid");
                var userCreater = await Usermanager.GetUserAsync(creator);
                control.UserCreator = userCreater;
                control.UserCreatorID = userCreater.Id;

                var usersIDs = controldto.ContorlUsersIDs;
                foreach (var id in usersIDs)
                {
                    ApplicationUser? user = context.Users.FirstOrDefault(u => u.Id == id);
                    if (user == null) return BadRequest("invalid member id");
                    ControlUsers memberControl = new ControlUsers()
                    {
                        ControlID = control.Id,
                        Control = control,
                        UserID = user.Id,
                        User = user,
                        JobType = JobType.Member
                    };
                    context.ControlUsers.Add(memberControl);
                }

                var subjectIDs = controldto.ControlSubjectsIDs;
                foreach (var id in subjectIDs)
                {
                    Subject? subject = context.Subject.FirstOrDefault(u => u.Id == id);
                    if (subject == null) return BadRequest("invalid subjet id");
                    ControlSubject cs = new ControlSubject();
                    cs.Subject = subject;
                    cs.SubjectID = subject.Id;
                    cs.Control = control;
                    cs.ControlID = control.Id;
                    control.ControlSubjects.Add(cs);
                    context.ControlSubject.Add(cs);
                }

                context.Control.Add(control);
                facultiy.Controls.Add(control);

               if(await context.SaveChangesAsync() > 0)
                    return Ok("Created Control");
               return BadRequest("Failed to add data");
            }
            return BadRequest("Faculty not found");
        }

        [HttpPut("edit/{Cid}")]
        public async Task<IActionResult> EditControl(ControlDTO controldto, string Cid)
        {
            var creator = ContextAccessor.HttpContext.User;
            if (creator == null) return BadRequest("Creator ID invalid");
            var userCreater = await Usermanager.GetUserAsync(creator);

            Control? control = context.Control.FirstOrDefault(c => c.Id == Cid);
            if (control == null) return BadRequest("inValid control id");

            if(userCreater.Id != control.UserCreatorID) return BadRequest("inValid creater id");

            Faculity? faculity = context.Faculity.FirstOrDefault(f => f.Id == control.FaculityID);
            if (faculity == null) return BadRequest("Control not has faculty");

            control.Name = controldto.Name;
            control.Faculity_Node = controldto.Faculity_Node;
            control.Faculity_Semester = controldto.Faculity_Semester;
            control.Faculity_Phase = controldto.Faculity_Phase;
            control.Start_Date = controldto.Start_Date;
            control.End_Date = controldto.End_Date;
            control.ACAD_YEAR = controldto.ACAD_YEAR;

            ControlUsers controlUsers = context.ControlUsers.FirstOrDefault(c => c.ControlID == control.Id);
            if (controlUsers == null) return BadRequest("Invalid control");
            ApplicationUser? mamager = context.Users.FirstOrDefault(u => u.Id == controldto.ControlManagerID);
            if (mamager == null) return BadRequest("Invalid Head of control id");
            controlUsers.UserID = mamager.Id;
            controlUsers.User = mamager;


            var usersIDs = controldto.ContorlUsersIDs;
            foreach (var id in usersIDs)
            {
                ApplicationUser? user = context.Users.FirstOrDefault(u => u.Id == id);
                if (user == null) return BadRequest("invalid member id");
                ControlUsers memberControl = new ControlUsers()
                {
                    ControlID = control.Id,
                    Control = control,
                    UserID = user.Id,
                    User = user,
                    JobType = JobType.Member
                };
                context.ControlUsers.Add(memberControl);
            }

            var subjectIDs = controldto.ControlSubjectsIDs;
            foreach (var id in subjectIDs)
            {
                Subject? subject = context.Subject.FirstOrDefault(u => u.Id == id);
                if (subject == null) return BadRequest("invalid subjet id");
                ControlSubject cs = new ControlSubject();
                cs.Subject = subject;
                cs.SubjectID = subject.Id;
                cs.Control = control;
                cs.ControlID = control.Id;
                control.ControlSubjects.Add(cs);
                context.ControlSubject.Add(cs);
            }

            context.Control.Add(control);
            faculity.Controls.Add(control);
            

            await context.SaveChangesAsync();


            return Ok("Created Control");
        }
        
        [HttpGet("allControllers")]
        public async Task<IActionResult> index()
        {
            var controlles = await context.Control.Include(c=>c.ControlSubjects).ToListAsync();
            if (controlles == null) return Ok(new List<Control>());
            return Ok(controlles);
        }
        
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> detail(string id)
        {
            var controle = await context.Control.Include(c=>c.ControlSubjects).SingleOrDefaultAsync(x => x.Id == id);
            if (controle == null) return BadRequest("Not Found");
            return Ok(controle);
        }
        
        [HttpDelete("delete/{id}")]
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
            var faculty = context.Faculity.Include(f=>f.Controls).ThenInclude(c=>c.ControlSubjects).FirstOrDefault(x => x.Id == Fid);
            if (faculty == null) return BadRequest("Faculty not found");
            if (faculty.Controls == null) return BadRequest("Faculty not has controls");
            return Ok(faculty.Controls);
        }

        [HttpGet("semeter/{Sid}/acadmec/{AY}")]
        public async Task<IActionResult> GetControlBySemesterAndAcademcYear(string Sid,string AY)
        {
            var control = context.Control.Include(c => c.ControlSubjects).Where(c => c.ACAD_YEAR == AY && c.Faculity_Semester == Sid);
            if (control == null) return BadRequest("Control not found");
            return Ok(control);
        }
    }
}
