using System.ComponentModel.DataAnnotations;

namespace CMS_back.Reposatory.Models
{
	public class Control_UserTasks
	{
		
		public string Control_TaskID { get; set; }
		public Control_Task Control_Task { get; set; }

		public string? UserTaskID {  get; set; }

		public ApplicationUser? UserTask {  get; set; }
	}
}
