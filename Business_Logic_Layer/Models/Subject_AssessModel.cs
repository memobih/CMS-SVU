namespace Business_Logic_Layer.Models
{
	public class Subject_AssessModel
	{
		public int ID { get; set; }
		public double MAX_Degree { get; set; }
		public double MIN_Degree { get; set; }
		public int? SubjectID { get; set; }
		public SubjectModel? Subject { get; set; }

	}
}
