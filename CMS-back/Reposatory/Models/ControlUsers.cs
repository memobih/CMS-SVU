using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{
	public enum JobType
	{
		Member,
		Head
	}
	public class ControlUsers
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? ControlID { get; set; }
		public Control? Control {  get; set; }
		public string? UserID { get; set; }
		public ApplicationUser? User { get; set; }
		
		public JobType JobType { get; set;}
	}
}
