namespace CMS_back.Reposatory.Models
{ 
    public class Subject_Assess
	{
		public string ID { get; set; }
		public double MAX_Degree { get; set; }
		public double MIN_Degree { get; set; }
		public string? SubjectID { get; set; }
		public Subject? Subject { get; set; }

	}
}
