using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Entities
{
	public class Faculity_Phases
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }
		public string? Code { get; set; }
		public string? Order { get; set; }
		public string? FaculityID { get; set; }
		public Faculity? Faculity { get; set; }

	}
}
