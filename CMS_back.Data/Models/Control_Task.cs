using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{

	public class Control_Task
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string Description { get; set; }
		public Question IsDone { get; set; }
		public DateTime? CreationDate { get; set; }
		public string CreateByID {  get; set; }
		public ApplicationUser CreateBy { get; set; }

		public string? ControlID { get; set; }
		public Control? Control { get; set; }

		public virtual ICollection<Control_UserTasks> UserTasks { get; set; }

	}
}
