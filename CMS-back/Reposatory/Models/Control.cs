using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_back.Models
{
	public class Control
	{

		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string Name { get; set; }
		public string? Faculity_Phase { get; set; }
		public string? Faculity_Node { get; set; }
		public string? Faculity_Semester { get; set; }
		public DateTime? Start_Date { get; set; }
		public DateTime? End_Date { get; set; }

		[RegularExpression(pattern: @"^\d{4}\/\d{4}$",ErrorMessage = "Invalid ACAD_YEAR Format. Please use the format 'YYYY/YYYY'.\"")]
		public string? ACAD_YEAR { get; set; }

		public virtual  ICollection<Control_Addresses>? conrol_Addresses  { get;}



		[ForeignKey("UserCreator")]
		public string UserCreatorID { get; set; }
		public virtual ApplicationUser UserCreator { get; set; }

		public string FaculityID { get; set; }
		public Faculity Faculity { get; set; }

		public virtual ICollection<ControlSubject> ControlSubjects { get; set; } 
		public virtual ICollection<ControlUsers>? ControlUsers { get; set; } 
	}
}
