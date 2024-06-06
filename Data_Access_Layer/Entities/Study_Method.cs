using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Entities
{
	public class Study_Method
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }
	}
}
