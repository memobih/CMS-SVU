namespace Business_Logic_Layer.Models
{
	public class ControlSubjectModel
	{
		public int ControlID { get; set; }
		public ControlModel Control { get; set; }
		public int SubjectID { get; set; }
		public SubjectModel Subject { get; set; }

	}
}
