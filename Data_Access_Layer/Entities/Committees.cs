using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Entities
{
	public class Committees
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }
		public DateTime? Time { get; set; }


		public string? FaculityNodeID { get; set; }
		public Faculity_Node? FaculityNode { get; set; }
	}
}
