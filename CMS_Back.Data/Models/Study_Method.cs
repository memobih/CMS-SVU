using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{
	public class Study_Method
	{
		[Key, MaxLength(200)]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }
	}
}
