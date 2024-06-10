using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{
	public class Control_UserTasks
	{
		//[Key]
		//public string Id { get; set; } = Guid.NewGuid().ToString();
		public string Control_TaskID { get; set; }
		public Control_Task Control_Task { get; set; }
		public string UserTaskID {  get; set; }
		public ApplicationUser UserTask {  get; set; }
        public Question IsDone { get; set; }
    }
}
