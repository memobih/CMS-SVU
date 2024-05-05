using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace CMS_back.Reposatory.Models
{
	public enum UserType
	{
		university_administrator,
		faculity_administrator,
		head_of_control,
		member_of_control
	}
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
		public string? UserImage { get; set; }
		//public string UserName { get; set; }
		//public string UserPassword { get; set; }
		public string ScientificDegree { get; set; }
		public UserType Type { get; set; }

		[InverseProperty("UserCreator")]
		public virtual ICollection<Control>? UserCreatorControls { get; } = new List<Control>();

		[ForeignKey("FaculityEmployee")]
		public string? FaculityEmployeeID { get; set; }
		public Faculity? FaculityEmployee { get; set; }

		[ForeignKey("MemberOfControl")]
		public string? MemberOfControlID { get; set; }
		public Control? MemberOfControl { get; set; }

		[InverseProperty("ControlManager")]
		public virtual ICollection<Control>? UserManagerControls { get; } = new List<Control>();
		public string? FaculityLeaderID {  get; set; }
		public Faculity? FaculityLeader { get; set; }
	}
}
