using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{
	public class Committees
	{
		[Key, MaxLength(200)]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }

		public DateTime? Time { get; set; }


		public string? FaculityNodeID { get; set; }
		public Faculity_Node? FaculityNode { get; set; }
	}
}
