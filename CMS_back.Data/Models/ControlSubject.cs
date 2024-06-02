using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{
	public class ControlSubject
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string ControlID { get; set; }
		public Control Control { get; set; }
		public string? SubjectID { get; set; }
		public Subject? Subject { get; set; }

	}
}
