using AutoMapper;
using CMS_back.Consts;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.IGenericRepository;
using CMS_back.Interfaces;
using CMS_back.Mailing;
using CMS_back.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace CMS_back.Services
{

    public class ControlRepository : IControlRepository
    {
        private readonly CMSContext _context;
        private readonly DbSet<Control> _dbSet;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Control> _genericRepository;
        private readonly IGenericRepository<Subject> _subjectRepository;
        public IHttpContextAccessor _contextAccessor;
        public UserManager<ApplicationUser> _usermanager;
        public IMailingService MailingService;
        public ControlRepository(CMSContext context, IMapper mapper, IGenericRepository<Control> genericRepository,
            IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> usermanager
            , IGenericRepository<Subject> subjectRepository, IMailingService mailingService)
        {
            _mapper = mapper;
            _context = context;
            _dbSet = _context.Set<Control>();
            _genericRepository = genericRepository;
            _contextAccessor = contextAccessor;
            _usermanager = usermanager;
            _subjectRepository = subjectRepository;
            MailingService = mailingService;
        }
        public async Task<ControlResultDto> GetByIdAsync(string id)
        {
            var control = await _dbSet.FindAsync(id);
            var controlResult = _mapper.Map<ControlResultDto>(control);
            return controlResult;
        }

        public async Task<IEnumerable<ControlResultDto>> GetAllAsync()
        {
            var user = _contextAccessor.HttpContext.User;
            var currentUser = await _usermanager.GetUserAsync(user);
            if (currentUser == null) throw new Exception("User Not Login in Any Control Yet");

            var controlOfCurrentUsers = await _genericRepository.FindAsync(c => c.UserCreatorID == currentUser.Id);
            var controlsResult = controlOfCurrentUsers.Select(c => _mapper.Map<ControlResultDto>(c)).ToList();
            return controlsResult;

        }


        public async Task<bool> AddAsync(ControlDTO controldto, string Fid)
        {
            var creator = _contextAccessor.HttpContext.User;
            var userCreater = await _usermanager.GetUserAsync(creator);
            if (userCreater == null) return false;
            var facultiy = _genericRepository.FindFirstAsync(c=>c.FaculityID == Fid);
            if (facultiy == null) return false;

                Control control = _mapper.Map<Control>(controldto);
                control.FaculityID = Fid;

                ApplicationUser? manager = await _usermanager.FindByIdAsync(controldto.ControlManagerID);
                if (manager == null) return false;
                ControlUsers userControl = new ControlUsers()
                {
                    ControlID = control.Id,
                    UserID = manager.Id,
                    JobType = JobType.Head
                };
                if (manager.Email != null)
                {
                    var message = new MailMessage(new string[] { manager.Email }, "Control System", "You are Head of new Control");
                    MailingService.SendMail(message);
                }
                _context.ControlUsers.Add(userControl);

                
                control.UserCreatorID = userCreater.Id;

                
                foreach (var id in controldto.UsersIds)
                {
                    ApplicationUser user = _context.Users.FirstOrDefault(u => u.Id == id);
                    if (user == null) return false;
                    ControlUsers memberControl = new ControlUsers()
                    {
                        ControlID = control.Id,
                        UserID = user.Id,
                        JobType = JobType.Member
                    };
                    if (user.Email != null)
                    {
                        var message = new MailMessage(new string[] { user.Email }, "Control System", "You are Member in new Control");
                        MailingService.SendMail(message);
                    }
                    _context.ControlUsers.Add(memberControl);
                }

 
                foreach (var id in controldto.SubjectsIds)
                {
                    Subject subject = _context.Subject.FirstOrDefault(u => u.Id == id);
                    if (subject == null) return false;
                    ControlSubject cs = new ControlSubject();
                    cs.SubjectID = subject.Id;
                    cs.ControlID = control.Id;
                    _context.ControlSubject.Add(cs);
                }

                _context.Control.Add(control);

                if (await _context.SaveChangesAsync() > 0)
                    return true;
                return false;
        }

        //public async Task UpdateAsync(ControlDTO controldto, string Cid)
        //{
        //    var creator = _contextAccessor.HttpContext.User;
        //    var userCreator = await _usermanager.GetUserAsync(creator);
        //    if (userCreator == null) throw new Exception("Creator ID invalid");

        //    var control = await _dbSet.FindAsync(Cid);
        //    if (control == null) throw new Exception("Invalid control Id");
        //    if (userCreator.Id != control.UserCreatorID) throw new Exception("Creator ID Invalid");

        //    control.ControlSubjects.Clear();
        //    control.ControlUsers.Clear();
        //    _mapper.Map(controldto, control);

        //    var subjects = new List<ControlSubject>();
        //    var subjectIDs = controldto.SubjectsIds;
        //    foreach (var id in subjectIDs)
        //    {
        //        var subject = _context.Subject.FirstOrDefault(u => u.Id == id);

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
        //        var user = _context.ApplicationUser.FirstOrDefault(c => c.Id == id);
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
        //        //var subject = _context.Subject.FirstOrDefault(id);
        //        var subject = await _subjectRepository.GetById(id);
        //        ControlSubject cs = new ControlSubject();
        //        cs.Subject = subject;
        //        cs.SubjectID = subject.Id;
        //        cs.Control = control;
        //        cs.ControlID = control.Id;
        //        subjects.Add(cs);
        //    }
        //    control.ControlSubjects = subjects;
        //    control.ControlUsers = controlUsers;

        //    _context.Attach(control);
        //    _context.Entry(control).State = EntityState.Modified;

        //    await _context.SaveChangesAsync();
        //}

        //public async Task<bool> DeleteAsync(string id)
        //{
        //    var control = await _dbSet.FindAsync(id);

        //    if (control == null)
        //    {
        //        return false;
        //    }

        //    _genericRepository.Remove(control);
        //    if (await _context.SaveChangesAsync() > 0)
        //        return true;

        //    return false;
        //}


        public async Task UpdateAsync(ControlDTO controldto, string Cid)
        {
            var userCreator = await _usermanager.GetUserAsync(_contextAccessor.HttpContext.User);
            if (userCreator == null)
                throw new InvalidOperationException("Invalid creator ID.");

            var control = await _genericRepository.FindFirstAsync(c => c.Id == Cid);
            if (control == null)
                throw new ArgumentException("Invalid control ID.");

            if (userCreator.Id != control.UserCreatorID)
                throw new InvalidOperationException("Unauthorized operation: creator ID mismatch.");

            control.ControlSubjects.Clear();
            control.ControlUsers.Clear();

            _mapper.Map(controldto, control);

            foreach (var id in controldto.SubjectsIds)
            {
                var subject = await _subjectRepository.GetById(id);
                if (subject != null)
                    control.ControlSubjects.Add(new ControlSubject { Subject = subject });
            }

            foreach (var id in controldto.UsersIds)
            {
                var user = await _context.ApplicationUser.FirstOrDefaultAsync(c => c.Id == id);
                if (user != null)
                    control.ControlUsers.Add(new ControlUsers { JobType = JobType.Member, User = user });
            }

            var controlManager = await _context.ApplicationUser.FirstOrDefaultAsync(c => c.Id == controldto.ControlManagerID);
            if (controlManager != null)
                control.ControlUsers.Add(new ControlUsers { JobType = JobType.Head, User = controlManager });

            _context.Update(control);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ControlResultDto>> GetControlsByAcadYearAsync(string AcadYear)
        {
            var controls = await _genericRepository.FindAsync(c => c.ACAD_YEAR == AcadYear);
            var controlsDTO = _mapper.Map<IEnumerable<ControlResultDto>>(controls);
            return controlsDTO;
        }

        public async Task<IEnumerable<ControlResultDto>> GetControlsByFaculityIdAsync(string FaculityId)
        {

            var controls = await _genericRepository.FindAsync(c => c.FaculityID == FaculityId);
            var controlsDTO = _mapper.Map<IEnumerable<ControlResultDto>>(controls);
            return controlsDTO;
        }
    }

}
