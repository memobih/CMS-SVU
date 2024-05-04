using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_back.Reposatory.Models
{
	public class Faculity_Node
	{
		public string ID { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string Order { get; set; }
		public string Level { get; set; }

		[ForeignKey("FaculityNode")]
		public string FaculityNodeID { get; set; }
		public Faculity FaculityNode { get; set; }

		public string? ParentID { get; set; }
		public Faculity_Node? Parent { get; set; }
		public virtual ICollection<Faculity_Node>? Faculity_Nodes { get; set; }
	}
}
