using System.ComponentModel.DataAnnotations;

namespace CMS_back.Reposatory.Models
{
	public class Student_SemesterSubjects
	{

		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public double? Degree { get; set; }
		public Question? IsPass { get; set; }

		public string? StudentSemesterID { get; set; }
		public StudentSemester? StudentSemester { get; set; }
		public string? SubjectID {  get; set; }
		public Subject? Subject { get; set; }


	}
}
