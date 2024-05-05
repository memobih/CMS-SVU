namespace CMS_back.Reposatory.Models
{
	public class ControlSubject
	{
		public string ControlID { get; set; }
		public Control Control { get; set; }
		public string? SubjectID { get; set; }
		public Subject? Subject { get; set; }

	}
}
