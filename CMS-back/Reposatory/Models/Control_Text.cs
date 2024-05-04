namespace CMS_back.Reposatory.Models
{
    public enum TextType
	{
		Note,
		Task
	}
	public class Control_Text
	{
		public string ID { get; set; }
		public string Description { get; set; }
		public Question IsDone { get; set; }
		public TextType Type { get; set; }
		public DateOnly CreationDate { get; set; }

		public List<ApplicationUser> AssignTo { get; set; }

		public ApplicationUser CreateBy {  get; set; }

		public string ControlID { get; set; }
		public Control Control { get; set; }

	}
}
