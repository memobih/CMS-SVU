using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Data_Access_Layer.Entities
{
	//public enum UserType
	//{
	//	FaculityAdministrator,
	//	UniversityAdministrator,
	//	Staff
	//}

	public class ApplicationUser : IdentityUser
	{

		public string Name { get; set; }
		public string? UserImage { get; set; }
		public string? ScientificDegree { get; set; }

		public string Type { get; set; }
        public string? OTP { get; set; }
        public DateTime OTPExpiry { get; set; }

        [InverseProperty("UserCreator")]
		public virtual ICollection<Control>? UserCreatorControls { get; } = new Collection<Control>();

		[ForeignKey("FaculityEmployee")]
		public string? FaculityEmployeeID { get; set; }
		public Faculity? FaculityEmployee { get; set; }
		public string? FaculityLeaderID {  get; set; }
		public Faculity? FaculityLeader { get; set; }
	}
}
