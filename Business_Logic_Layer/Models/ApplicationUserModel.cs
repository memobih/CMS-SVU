using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Business_Logic_Layer.Models
{
	public enum UserType
	{
		MemberOfControl,
		HeadOfControl,
		FaculityAdministrator,
		UniversityAdministrator
	}
	public class ApplicationUserModel : IdentityUser
	{
		public string Name { get; set; }
		public string? UserImage { get; set; }
		public string UserName { get; set; }
		
		public string UserPassword { get; set; }
		public string ScientificDegree { get; set; }

		public UserType Type { get; set; }

		[InverseProperty("UserCreator")]
		public virtual ICollection<ControlModel>? UserCreatorControls { get; } = new List<ControlModel>();

		[ForeignKey("FaculityEmployee")]
		public int? FaculityEmployeeID { get; set; }
		public FaculityModel? FaculityEmployee { get; set; }

		[ForeignKey("MemberOfControl")]
		public int? MemberOfControlID { get; set; }
		public ControlModel? MemberOfControl { get; set; }

		[InverseProperty("ControlManager")]
		public virtual ICollection<ControlModel>? UserManagerControls { get; } = new List<ControlModel>();

	
		public int? FaculityLeaderID {  get; set; }
		public FaculityModel? FaculityLeader { get; set; }
	}
}
