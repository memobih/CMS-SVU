namespace CMS_back.Reposatory.Models
{
	public class StudentSubject
	{
		public string StudentID { get; set; }
		public Student Student { get; set; }
		public string SubjectID { get; set; }
		public Subject Subject { get; set; }

	}
}
