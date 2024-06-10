using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{

	public class FaculityType
	{
		[Key, MaxLength(200)]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }  //Type

	}
}
