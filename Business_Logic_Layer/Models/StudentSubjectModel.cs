namespace Business_Logic_Layer.Models
{
	public class StudentSubjectModel
	{
		public int StudentID { get; set; }
		public StudentModel Student { get; set; }
		public int SubjectID { get; set; }
		public SubjectModel Subject { get; set; }

	}
}
