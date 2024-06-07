using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_back.Models
{
	public class Faculity
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }
		public string? Code { get; set; }
		public string? Order { get; set; }
		public string? FaculityTypeID { get; set; }
		public FaculityType? FaculityType { get; set; }

		public virtual ICollection<Faculity_Phases>? Phases { get;  } = new List<Faculity_Phases>();
		public virtual ICollection<Faculity_Semester>? Semesters { get; } = new List<Faculity_Semester>();
		[InverseProperty("Faculity")]
		public virtual ICollection<Faculity_Node>? Nodes { get; } = new List<Faculity_Node>();

		[InverseProperty("FaculityEmployee")]
		public virtual ICollection<ApplicationUser> Users { get; set; }
		public virtual ICollection<Control> Controls { get; }=new List<Control>();
		public virtual ICollection<Staff>? Staff { get; }


		[ForeignKey("UserLeader")]
		public string? UserLeaderID { get; set; }

		[InverseProperty("FaculityLeader")]
		public ApplicationUser? UserLeader { get; set; }


	}
}
