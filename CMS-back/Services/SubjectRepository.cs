using CMS_back.Data;
using CMS_back.DTO;
using CMS_back.GenericRepository;
using CMS_back.IGenericRepository;
using CMS_back.Interfaces;
using CMS_back.Models;
using System.Security.Cryptography;

namespace CMS_back.Services
{
    public class SubjectRepository : ISubjectRepository
    {
        public IGenericRepository<Faculity_Node> _facultyNodeGeneric {  get; }
        public IGenericRepository<ControlSubject> _controlSubjectGeneric { get; }
        public CMSContext Context { get; }

        public SubjectRepository(CMSContext _context) 
        { 
            Context=_context;
            _facultyNodeGeneric = new GenericRepository<Faculity_Node>(_context);
            _controlSubjectGeneric = new GenericRepository<ControlSubject>(_context);
        }
        public async Task<ICollection<Subject>?> GetFacultySubject(string facultyId)
        {
            var facultyNode = await _facultyNodeGeneric.FindAsync(x => x.FaculityNodeID == facultyId, ["Subjects"]);
            if(facultyNode == null) { return null; }
            var subjects = facultyNode.Select(f => f.Subjects);
            return subjects.SelectMany(list => list).ToList(); 
        }
        public async Task<IEnumerable<Subject?>?> GetControlSubject(string controlId)
        {
            var controlSubjects = await _controlSubjectGeneric.FindAsync(c => c.ControlID == controlId, ["Subject"]);
            if(controlSubjects == null) { return null; }
            var subjects = controlSubjects.Select(c => c.Subject);
            return subjects;
        }
        public async Task<Subject?> AddSubject(Subject subject)
        {
            var isExict = Context.Subject.FirstOrDefault(s => s.Code == subject.Code);
            if(isExict != null) { return null; }
            // chack if need to add in faculty node or not
            Context.Subject.Add(subject);
            await Context.SaveChangesAsync();
            return subject;
        }
        public async Task<Subject?> FinishedSubject(string subjectId)
        {
            var subject = Context.Subject.FirstOrDefault(s => s.Id == subjectId);
            if (subject == null) return null;
            subject.IsDone = Question.Yes;
            await Context.SaveChangesAsync();
            return subject;
        }
        public async Task<Subject?> ReviewSubject(string subjectId)
        {
            var subject = Context.Subject.FirstOrDefault(s => s.Id == subjectId);
            if (subject == null) return null;
            subject.IsReview = Question.Yes;
            await Context.SaveChangesAsync();
            return subject;
        }
    }
}
