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
        public ControlsController(IControlRepository repo,/*IRepository<Subject> subjectrepository, IRepository<ControlSubject> controlsubjectrepository,*/ CMSContext _context, IConfiguration _cfg,
            UserManager<ApplicationUser> usermanager, IHttpContextAccessor contextAccessor, IMapper mapper, IMailingService mailingService)
        {
            _repo = repo;
            //_Isubjectrepository = subjectrepository;
            //_Icontrolsubjectrepository = controlsubjectrepository;
            context = _context;
            cfg = _cfg;
            Usermanager = usermanager;
            ContextAccessor = contextAccessor;
            _mapper = mapper;
            MailingService = mailingService;
        }

        [HttpPost("create/{Fid}")]
        [Authorize(Roles = ConstsRoles.AdminFaculty)]
        public async Task<IActionResult> createControl(ControlDTO controldto, string Fid)
        {
           var result =await _repo.AddAsync(controldto, Fid);
            return result ?Ok(result) : BadRequest("Invalid Control Data");
        }



        [HttpPut("edit")]
        [Authorize(Roles = ConstsRoles.AdminFaculty)]
        public async Task<IActionResult> EditControl(ControlDTO controldto, string Cid)
        {

            var control = await _repo.GetByIdAsync(Cid);
            if (control == null) return BadRequest("Not Found");

            _repo.UpdateAsync(controldto, Cid);
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
