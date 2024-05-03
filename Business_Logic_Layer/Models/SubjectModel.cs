namespace Business_Logic_Layer.Models
{
	public enum Question
	{
		Yes,
		No
	}
	public class SubjectModel
	{
		public int ID { get; set; }
		public string Name { get; set; }

		public Question IsDone { get; set; }
		public Question IsReview { get; set; }		
		public string Code { get; set; }
		public int Credit_Hours { get; set; }
		public int? FaculitySemesterID { get; set; }
		public Faculity_SemesterModel? Faculity_Semester { get; set; }

		public int FaculityPhaseID { get; set; }
		public Faculity_PhasesModel Faculity_Phases { get; set; }

		public virtual ICollection<Subject_AssessModel>? subject_Assesses { get; } = new List<Subject_AssessModel>();

		public virtual ICollection<ControlSubjectModel> ControlSubjects { get; } = [];

		public virtual ICollection<StudentSubjectModel> StudentSubjects { get; } = [];

	}
}
