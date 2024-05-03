namespace Data_Access_Layer.Reposatory.Entities
{
	public enum Question
	{
		Yes,
		No
	}
	public class Subject
	{
		public int ID { get; set; }
		public string Name { get; set; }

		public Question IsDone { get; set; }
		public Question IsReview { get; set; }		
		public string Code { get; set; }
		public int Credit_Hours { get; set; }
		public int? FaculitySemesterID { get; set; }
		public Faculity_Semester? Faculity_Semester { get; set; }

		public int FaculityPhaseID { get; set; }
		public Faculity_Phases Faculity_Phases { get; set; }

		public virtual ICollection<Subject_Assess>? subject_Assesses { get; } = new List<Subject_Assess>();

		public virtual ICollection<ControlSubject> ControlSubjects { get; } = [];

		public virtual ICollection<StudentSubject> StudentSubjects { get; } = [];

	}
}
