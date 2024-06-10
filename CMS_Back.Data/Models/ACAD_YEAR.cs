using System.ComponentModel.DataAnnotations;

namespace CMS_back.Models
{
	public class ACAD_YEAR
	{
        [Key, MaxLength(200)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

		[RegularExpression(pattern: @"^\d{4}\/\d{4}$", ErrorMessage = "Invalid ACAD_YEAR Format. Please use the format 'YYYY/YYYY'.\"")]
		public string? Name { get; set; }

		public virtual ICollection<StudentSemester>? StudentSemesters { get;}


	}
}
