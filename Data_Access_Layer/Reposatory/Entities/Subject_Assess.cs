namespace Data_Access_Layer.Reposatory.Entities
{ 
    public class Subject_Assess
	{
		public int ID { get; set; }
		public double MAX_Degree { get; set; }
		public double MIN_Degree { get; set; }
		public int? SubjectID { get; set; }
		public Subject? Subject { get; set; }

	}
}
