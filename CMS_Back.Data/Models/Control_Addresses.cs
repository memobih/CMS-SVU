using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{
	public class Control_Addresses
	{
		//[Key]
		//public string Id { get; set; } = Guid.NewGuid().ToString();
		public string ControlID { get; set; }
		public Control Control { get; set; }
		public string Address { get; set; }

		
	}
}
