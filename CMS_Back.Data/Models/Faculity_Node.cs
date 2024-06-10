using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_back.Models
{
	public class Faculity_Node
	{
		[Key, MaxLength(200)]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }
		public string? Code { get; set; }
		public string? Order { get; set; }
		public string? Level { get; set; }

		[ForeignKey("Faculity")]
		public string FaculityID { get; set; }
		public Faculity Faculity { get; set; }

		public string? ParentID { get; set; }
		public Faculity_Node? Parent { get; set; }
		public virtual ICollection<Faculity_Node>? Faculity_Nodes { get; set; }
		public virtual ICollection<Committees>? Committees { get; set; }
		public virtual ICollection<StudentSemester>? StudentSemesters { get; set; }
		public virtual ICollection<Subject>? Subjects { get; } = new List<Subject>();
	}
}
