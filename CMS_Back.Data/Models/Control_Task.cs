using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_back.Models
{

	public class Control_Task
	{
		[Key, MaxLength(200)]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string Description { get; set; }
        //[Column(TypeName = "Date")]
        public DateOnly? CreationDate { get; set; }
		public string CreateByID {  get; set; }
		public ApplicationUser CreateBy { get; set; }
		public string ControlID { get; set; }
		public Control Control { get; set; }
		public virtual ICollection<Control_UserTasks> UserTasks { get; set; }

	}
}
