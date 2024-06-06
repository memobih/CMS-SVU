using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Entities
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
