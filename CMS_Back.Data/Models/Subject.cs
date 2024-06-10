using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{
	public class Subject
	{
		[Key, MaxLength(200)]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }
		public string? Code { get; set; }
		public int? Credit_Hours { get; set; }
		public string FaculityNodeID { get; set; }
		public Faculity_Node FaculityNode { get; set; }
		public string? FaculityHierarycalID { get; set; }
		public FaculityHierarycal? FaculityHierarycal { get; set; }
		public virtual ICollection<Subject_Assess>? subject_Assesses { get; } 
		public virtual ICollection<ControlSubjects>? ControlSubjects { get; }
		public virtual ICollection<Subject_Committees>? Subject_Committees { get; }



	}
}
