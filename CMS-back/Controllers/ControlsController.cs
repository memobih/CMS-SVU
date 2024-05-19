using Microsoft.AspNetCore.Mvc;
using CMS_back.DTO;
using CMS_back.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CMS_back.Models;
using AutoMapper;
using CMS_back.Consts;
using CMS_back.Mailing;
using CMS_back.Interfaces;

namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ControlsController : ControllerBase
    {
        private readonly IControlRepository _repo;
        //private readonly IRepository<Subject> _Isubjectrepository;

        //private readonly IRepository<ControlSubject> _Icontrolsubjectrepository;

        public CMSContext context;
        public IConfiguration cfg;
        public UserManager<ApplicationUser> Usermanager;
        public IHttpContextAccessor ContextAccessor;
        public IMailingService MailingService;

        private readonly IMapper _mapper;
        public ControlsController(IControlRepository repo,/*IRepository<Subject> subjectrepository, IRepository<ControlSubject> controlsubjectrepository,*/ CMSContext _context,IConfiguration _cfg, 
            UserManager<ApplicationUser> usermanager, IHttpContextAccessor contextAccessor,IMapper mapper, IMailingService mailingService)
        {
            _repo = repo;
            //_Isubjectrepository = subjectrepository;
            //_Icontrolsubjectrepository = controlsubjectrepository;
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
        //    var facultiy = await _repo.GetByIdAsync(Fid);
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

        //        ApplicationUser? manager = context.Users.FirstOrDefault(u => u.Id == controldto.ControlManagerID);
        //        if (manager == null) return BadRequest("Invalid Head of control id");
        //        ControlUsers userControl = new ControlUsers()
        //        {
        //            ControlID = control.Id,
        //            Control = control,
        //            UserID = manager.Id,
        //            User = manager,
        //            JobType = JobType.Head
        //        };
        //        if (manager.Email != null)
        //        {
        //            var message = new Mailing.MailMessage(new string[] { manager.Email }, "Control System", "You are Head of new Control");
        //            MailingService.SendMail(message);
        //        }
        //        context.ControlUsers.Add(userControl);

        //        var creator = ContextAccessor.HttpContext.User;
        //        if (creator == null) return BadRequest("Creator ID invalid");
        //        var userCreater = await Usermanager.GetUserAsync(creator);
        //        control.UserCreator = userCreater;
        //        control.UserCreatorID = userCreater.Id;

        //        var usersIDs = controldto.UsersIds;
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

        //        var subjectIDs = controldto.SubjectsIds;
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
        //       // facultiy.Controls.Add(control);

        //        if (await context.SaveChangesAsync() > 0)
        //            return Ok("Created Control");
        //        return BadRequest("Failed to add data");
        //    }
        //    return BadRequest("Faculty not found");
        //}



        [HttpPut("edit")]
        [Authorize(Roles = ConstsRoles.AdminFaculty)]
        public async Task<IActionResult> EditControl(ControlDTO controldto, string Cid)
        {
            //{
            //    var creator = ContextAccessor.HttpContext.User;
            //    var userCreator = await Usermanager.GetUserAsync(creator);
            //    if (userCreator == null) return BadRequest("Creator ID invalid");

            //    //Control? control = context.Control.Include(c=>c.ControlSubjects).Include(u=>u.ControlUsers).ThenInclude(u=>u.User).FirstOrDefault(c => c.Id == Cid);
            //    var control = await _repo.GetByIdAsync(Cid);

            //    if (control == null) return BadRequest("Invalid control id");
            //    if (userCreator.Id != control.UserCreatorID) return BadRequest("Creator ID Invalid");

            //    control.ControlUsers.Clear();
            //    control.ControlSubjects.Clear();
            //    _mapper.Map(controldto, control);
            //    var subjects = new List<ControlSubject>();

            //    var subjectIDs = controldto.SubjectsIds;
            //    foreach (var id in subjectIDs)
            //    {
            //        var subject = context.Subject.FirstOrDefault(u => u.Id == id);

            //        ControlSubject cs = new ControlSubject();
            //        cs.Subject = subject;
            //        cs.SubjectID = subject.Id;
            //        cs.Control = control;
            //        cs.ControlID = control.Id;
            //        subjects.Add(cs);

            //    }
            //    var controlUsers = new List<ControlUsers>();
            //    foreach (var id in controldto.UsersIds)
            //    {
            //        var user = context.ApplicationUser.FirstOrDefault(c => c.Id == id);
            //        ControlUsers c = new ControlUsers
            //        {
            //            JobType = JobType.Member,
            //            ControlID = control.Id,
            //            UserID = user.Id
            //        };
            //        controlUsers.Add(c);
            //    }
            //    ControlUsers cu = new ControlUsers
            //    {
            //        JobType = JobType.Head,
            //        ControlID = control.Id,
            //        UserID = controldto.ControlManagerID
            //    };
            //    controlUsers.Add(cu);
            //    foreach (var id in subjectIDs)
            //    {
            //        var subject = _Irepository.GetByIdAsync(id);
            //        ControlSubject cs = new ControlSubject();
            //        cs.Subject = subject;
            //        cs.SubjectID = subject.Id;
            //        cs.Control = control;
            //        cs.ControlID = control.Id;
            //        subjects.Add(cs);
            //    }
            //    control.ControlSubjects = subjects;
            //    control.ControlUsers = controlUsers;
            //    context.Control.Update(control);
            var control = await _repo.GetByIdAsync(Cid);
            if (control == null) return BadRequest("Not Found");
            return Ok("Updated Control");
        }


        [HttpGet("allControllers")]
        public async Task<IActionResult> index()
        {
            //var user = ContextAccessor.HttpContext.User;
            //var currentUser = await Usermanager.GetUserAsync(user);
            //if (currentUser == null) return BadRequest("No user Login yet");

           
            //var controlOfCurrentUsers = await _repo.GetByIdAsync(currentUser.Id);
            //var controls = await _repo.GetByIdAsync(currentUser.Id).GetAllAsync();/*.Where(c=>c.UserID==currentUser.Id).Select(c=>c.Control);*/
           
            //var controlesResult=controls.Select(c=>_mapper.Map<ControlResultDto>(c)).ToList();
            //return Ok(controlesResult);

            var controls = await _repo.GetAllAsync();
            return Ok(controls);
        }
        
        [HttpGet("detail")]
        public async Task<IActionResult> detail(string id)
        {
            var control = await _repo.GetByIdAsync(id);
            if (control == null) 
                return BadRequest("Not Found");

            return Ok(control);
        }
        
        [HttpDelete("delete")]
        [Authorize(Roles = ConstsRoles.AdminFaculty)]
        public async Task<IActionResult> delete(string id)
        {
            var result = await _repo.DeleteAsync(id);
            return result ? Ok("Deleted Successfuly") : BadRequest("Can Not Delete This Control");
        }

        //[HttpGet("{Fid}")]
        [HttpGet("get-by-faculity-id")]
        public async Task<IActionResult> get (string Fid)
        {
            var controls = await _repo.GetControlsByFaculityIdAsync(Fid);

            //if (controls == null || !controls.Any())
            //{
            //    return BadRequest("No controls found for the specified faculty");
            //}

            //// Mapping entities to DTOs
            //var controlsDTO = _mapper.Map<IEnumerable<ControlResultDto>>(controls);
            //return Ok(controlsDTO);

            return Ok(controls);
        }

        [HttpGet("acadmec-year")]
        public async Task<IActionResult> GetControlsAcademcYear(string AcadYear)
        {
            var controls = await _repo.GetControlsByAcadYearAsync(AcadYear);
            //if (control == null) return BadRequest("Control not found");
            //var controlsDTO = _mapper.Map<IEnumerable<ControlResultDto>>(control);

            return Ok(controls);
        }
    }
}
