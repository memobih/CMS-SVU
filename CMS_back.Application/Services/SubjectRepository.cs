using AutoMapper;
using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.IGenericRepository;
using CMS_back.Interfaces;
using CMS_back.Models;
using System.Linq.Expressions;

namespace CMS_back.Services
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly CMSContext _context;
        private readonly IGenericRepository<Faculity_Node> _facultyNodeRepo;
        private readonly IGenericRepository<ControlSubjects> _controlSubjectGeneric;
        private readonly IMapper _mapper;

        public SubjectRepository(CMSContext context,IGenericRepository<Faculity_Node> facultyNodeRepo,IMapper mapper
            ,IGenericRepository<ControlSubjects> controlSubjectGeneric)
        {
            _context = context;      
            _facultyNodeRepo = facultyNodeRepo;
            _controlSubjectGeneric = controlSubjectGeneric;
            _mapper = mapper;
        }

        public async Task<IEnumerable<controlSubjectResultDTO>> GetFacultySubject(string facultyId)
        {
            var facultyNode = await _facultyNodeRepo.FindAsync(x => x.FaculityID == facultyId, "Subjects");
            if (facultyNode == null) throw new Exception("Not Found FaculityNode"); 
            var subjects = facultyNode.SelectMany(f => f.Subjects);
            var subjectsResult = _mapper.Map<IEnumerable<controlSubjectResultDTO>>(subjects).ToList();
            return subjectsResult;
        }

        public async Task<IEnumerable<controlSubjectResultDTO>> GetControlSubject(string controlId)
        {
            var controlSubjects = await _controlSubjectGeneric.FindAsync(c => c.ControlID == controlId, "Subject");
            if (controlSubjects == null) throw new Exception("Not Found ControlSubjects");
            var subjects = controlSubjects.Select(c => c.Subject);
            var subjectsResult = _mapper.Map<IEnumerable<controlSubjectResultDTO>>(subjects).ToList();
            return subjectsResult;
        }

        public async Task<bool> AddSubject(subjectDTO subjectdto)
        {
            var isExict = _context.Subject.FirstOrDefault(s => s.Code == subjectdto.Code);
            if (isExict != null) throw new Exception("This Subject Already Exist");

            var facultyNode = await _facultyNodeRepo.GetById(subjectdto.FaculityNodeID);
            if (facultyNode == null) throw new Exception("FacultyNode not found");
            Subject subjectresult = _mapper.Map<Subject>(subjectdto);
            _context.Subject.Add(subjectresult);
            if(await _context.SaveChangesAsync() >0) return true;
            return false;
        }

        public async Task<bool> FinishedSubject(string controlId,string subjectId)
        {
            Expression<Func<ControlSubjects, bool>> condition1 = c => c.ControlID == controlId;
            Expression<Func<ControlSubjects, bool>> condition2 = c => c.SubjectID == subjectId;
            var combinedCondition = condition1.AndAlso(condition2);

            var subject = await _controlSubjectGeneric.FindFirstAsync(combinedCondition);
            if (subject == null) throw new Exception("Subject Not Found in Control");
            subject.IsDone = Question.Yes;
            if (await _context.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<bool> ReviewSubject(string controlId, string subjectId)
        {
            Expression<Func<ControlSubjects, bool>> condition1 = c => c.ControlID == controlId;
            Expression<Func<ControlSubjects, bool>> condition2 = c => c.SubjectID == subjectId;
            var combinedCondition = condition1.AndAlso(condition2);

            var subject = await _controlSubjectGeneric.FindFirstAsync(combinedCondition);
            if (subject == null || subject.ControlID != controlId) throw new Exception("Subject Not Found in Control");
            subject.IsReview = Question.Yes;
            if (await _context.SaveChangesAsync() > 0) return true;
            return false;
        }

    }
}
