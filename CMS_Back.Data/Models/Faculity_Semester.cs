using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{
	public class Faculity_Semester
	{
		[Key, MaxLength(200)]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }
		public string? Code { get; set; }
		public string? Order {  get; set; }
		
		public string? FaculityID { get; set; }
		public Faculity? Faculity { get; set; }


	}
}
