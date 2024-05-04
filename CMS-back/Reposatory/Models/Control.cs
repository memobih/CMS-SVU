using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_back.Reposatory.Models
{
	public class Control
	{
		
		public string ID { get; set; }
		public string Name { get; set; }
		public string? Faculity_Phase { get; set; }
		public string? Faculity_Node { get; set; }
		public string Faculity_Semester { get; set; }
		public DateOnly Start_Date { get; set; }
		public DateOnly? End_Date { get; set; }

		[RegularExpression(pattern: @"^\d{4}\/\d{4}$",ErrorMessage = "Invalid ACAD_YEAR Format. Please use the format 'YYYY/YYYY'.\"")]
		public string ACAD_YEAR { get; set; }

		public ICollection<Control_Addresses> conrol_Addresses;
		public ICollection<Control_Text> Texts { get; } = new List<Control_Text>();
		
		[InverseProperty("MemberOfControl")]
		public ICollection<ApplicationUser> Users { get; } = new List<ApplicationUser>();

		[ForeignKey("UserCreator")]
		public string UserCreatorID { get; set; }
		public ApplicationUser UserCreator { get; set; }
		public string FaculityID { get; set; }
		public Faculity Faculity { get; set; }

		[ForeignKey("ControlManager")]
		public string ControlManagerID { get; set; }
		public ApplicationUser ControlManager { get; set; }
		public virtual ICollection<ControlSubject> ControlSubjects { get; } = [];
	}
}
