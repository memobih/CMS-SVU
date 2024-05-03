namespace Data_Access_Layer.Reposatory.Entities
{
	public class StudentSubject
	{
		public int StudentID { get; set; }
		public Student Student { get; set; }
		public int SubjectID { get; set; }
		public Subject Subject { get; set; }

	}
}
