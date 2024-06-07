using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{
	//public enum Type
	//{
	//	Affliate, // "انتساب"
	//	Regular, // "انتظام"
	//	AffliateAndRegular //"انتساب وانتظام"
	//}

	public class FaculityType
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }  //Type

	}
}
