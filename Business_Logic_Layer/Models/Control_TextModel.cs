namespace Business_Logic_Layer.Models
{
	public enum TextType
	{
		Note,
		Task
	}
	public class Control_TextModel
	{
		public int ID { get; set; }
		public string Description { get; set; }
		public Question IsDone { get; set; }
		public TextType Type { get; set; }
		public DateOnly CreationDate { get; set; }

		public int AssignTo { get; set; }
		public int CreateBy {  get; set; }

		public int ControlID { get; set; }
		public ControlModel Control { get; set; }

	}
}
