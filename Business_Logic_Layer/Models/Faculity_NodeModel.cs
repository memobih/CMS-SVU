using System.ComponentModel.DataAnnotations.Schema;

namespace Business_Logic_Layer.Models
{
	public class Faculity_NodeModel
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string Order { get; set; }
		public string Level { get; set; }

		[ForeignKey("FaculityNode")]
		public int FaculityNodeID { get; set; }
		public FaculityModel FaculityNode { get; set; }

		public int? ParentID { get; set; }
		public Faculity_NodeModel? Parent { get; set; }
		public virtual ICollection<Faculity_NodeModel>? Faculity_Nodes { get; set; }
	}
}
