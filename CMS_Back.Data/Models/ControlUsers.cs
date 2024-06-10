using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_back.Models
{
	public class ControlUsers
	{
		//[Key]
		//public string Id { get; set; } = Guid.NewGuid().ToString();
		public string ControlID { get; set; }
		[ForeignKey(nameof(ControlID))]
		public Control Control {  get; set; }
		public string UserID { get; set; }
		public ApplicationUser User { get; set; }
		
		public string JobType { get; set;}
	}
}
