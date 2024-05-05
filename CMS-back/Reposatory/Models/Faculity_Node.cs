using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_back.Reposatory.Models
{
	public class Faculity_Node
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }
		public string? Code { get; set; }
		public string? Order { get; set; }
		public string? Level { get; set; }

		[ForeignKey("FaculityNode")]
		public string FaculityNodeID { get; set; }
		public Faculity FaculityNode { get; set; }

		public string? ParentID { get; set; }
		public Faculity_Node? Parent { get; set; }
		public virtual ICollection<Faculity_Node>? Faculity_Nodes { get; set; }
		public virtual ICollection<Committees>? Committees { get; set; }
		public virtual ICollection<StudentSemester>? StudentSemesters { get; set; }
		public virtual ICollection<Subject>? Subjects { get; } = new List<Subject>();
	}
}
