using System.ComponentModel.DataAnnotations;

namespace CMS_back.Reposatory.Models
{
	public enum JobType
	{
		Member,
		Head
	}
	public class ControlUsers
	{
	
		public string ControlID { get; set; }
		public Control Control {  get; set; }
		public string? UserID { get; set; }
		public ApplicationUser? User { get; set; }
		
		public JobType JobType { get; set;}
	}
}
