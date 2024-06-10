using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{
	public class StudentSemester
	{
		[Key, MaxLength(200)]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public double? GPA { get; set; }
		public double? TOTAL { get; set; }
		public Question? IsPass { get; set; }
		public double? Percentage { get; set; }
		public string? StudentStatusID { get; set; }
		public Student_STATUS? StudentStatus { get; set; }

		public string? FaculityNodeID { get; set; }
		public Faculity_Node? FaculityNode { get; set;}
		public string? StudentID { get; set; }
		public Student? Student { get; set; }
		public string? AcadYearID { get; set; }
		public ACAD_YEAR? AcadYear { get; set; }
		public string? FaculityHierarcalID { get; set; }
		public FaculityHierarycal? FaculityHierarcal { get; set; }


	}
}
