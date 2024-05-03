using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Reposatory.Entities
{
	public class Faculity_Node
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string Order { get; set; }
		public string Level { get; set; }

		[ForeignKey("FaculityNode")]
		public int FaculityNodeID { get; set; }
		public Faculity FaculityNode { get; set; }

		public int? ParentID { get; set; }
		public Faculity_Node? Parent { get; set; }
		public virtual ICollection<Faculity_Node>? Faculity_Nodes { get; set; }
	}
}
