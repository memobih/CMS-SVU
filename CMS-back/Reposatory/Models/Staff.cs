using System.ComponentModel.DataAnnotations;

namespace CMS_back.Reposatory.Models
{
	public class Staff
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? NotionalID { get; set; }
		public string? Address { get; set; }
		public string? FaculityID { get; set; }
		public Faculity? Faculity { get; set; }


	}
}
