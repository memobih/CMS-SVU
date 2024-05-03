using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business_Logic_Layer.Models
{
	public class ControlModel
	{
		
		public int ID { get; set; }
		public string Name { get; set; }
		public string? Faculity_Phase { get; set; }
		public string? Faculity_Node { get; set; }
		public string Faculity_Semester { get; set; }
		public DateOnly Start_Date { get; set; }
		public DateOnly? End_Date { get; set; }

		[RegularExpression(pattern: @"^\d{4}\/\d{4}$",ErrorMessage = "Invalid ACAD_YEAR Format. Please use the format 'YYYY/YYYY'.\"")]
		public string ACAD_YEAR { get; set; }

		public ICollection<Control_AddressesModel> conrol_Addresses;
		public ICollection<Control_TextModel> Texts { get; } = new List<Control_TextModel>();
		
		[InverseProperty("MemberOfControl")]
		public ICollection<ApplicationUserModel> Users { get; } = new List<ApplicationUserModel>();

		[ForeignKey("UserCreator")]
		public string UserCreatorID { get; set; }
		public ApplicationUserModel UserCreator { get; set; }

		public int FaculityID { get; set; }
		public FaculityModel Faculity { get; set; }

		[ForeignKey("ControlManager")]
		public string ControlManagerID { get; set; }
		public ApplicationUserModel ControlManager { get; set; }
		public virtual ICollection<ControlSubjectModel> ControlSubjects { get; } = [];
	}
}
