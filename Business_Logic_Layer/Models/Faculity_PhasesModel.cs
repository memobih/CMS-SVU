namespace Business_Logic_Layer.Models
{
	public class Faculity_PhasesModel
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string Order { get; set; }
		public int FaculityID { get; set; }
		public FaculityModel Faculity { get; set; }

		public virtual ICollection<SubjectModel>? Subjects { get; } = new List<SubjectModel>();
	}
}
