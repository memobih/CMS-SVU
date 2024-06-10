using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{
	public class Subject_Committees
	{
		//[Key]
		//public string Id { get; set; } = Guid.NewGuid().ToString();
		public string SubjectID { get; set; }
		public Subject Subject { get; set; }
		public string CommitteeID { get; set; }
		public Committees Committee { get; set; }
	}
}
