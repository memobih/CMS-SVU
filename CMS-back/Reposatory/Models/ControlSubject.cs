namespace CMS_back.Reposatory.Models
{
	public class ControlSubject
	{
		public int ControlID { get; set; }
		public Control Control { get; set; }
		public int SubjectID { get; set; }
		public Subject Subject { get; set; }

	}
}
