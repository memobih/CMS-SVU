namespace Data_Access_Layer.Reposatory.Entities
{
	public enum TextType
	{
		Note,
		Task
	}
	public class Control_Text
	{
		public int ID { get; set; }
		public string Description { get; set; }
		public Question IsDone { get; set; }
		public TextType Type { get; set; }
		public DateOnly CreationDate { get; set; }

		public int AssignTo { get; set; }
		public int CreateBy {  get; set; }

		public int ControlID { get; set; }
		public Control Control { get; set; }

	}
}
