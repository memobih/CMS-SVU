using System.ComponentModel.DataAnnotations;

namespace CMS_back.Reposatory.Models
{
	public class Assess
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }

		public virtual ICollection<Subject_Assess>? Subject_Assesss { get; set; }
	}
}
