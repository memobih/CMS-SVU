using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Entities
{
	public class BYLAW
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }
		public string? StudyMethodID { get; set; }
		public Study_Method? StudyMethod { get; set; }
		public string? FaculityID { get; set; }
		public Faculity? Faculity { get; set; }

	}
}
