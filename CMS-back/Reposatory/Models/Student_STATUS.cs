using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{
	public enum StudentStatus
	{
		Remaining,
		Freshman,
		International
	}
	public class Student_STATUS
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public StudentStatus? Student_Status { get; set; }
	}
}
