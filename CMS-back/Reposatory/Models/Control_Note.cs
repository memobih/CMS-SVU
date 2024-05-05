using System.ComponentModel.DataAnnotations;

namespace CMS_back.Reposatory.Models
{
	public class Control_Note
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string Description { get; set; }
		public DateOnly? WriteDate { get; set; }
		public string WriteByID { get; set; }
		public ApplicationUser WriteBy { get; set; }

		public string ControlID { get; set; }
		public Control Control { get; set; }

	}
}
