using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Reposatory.Entities
{
	public enum FaculityType
	{
		Affliate, // "انتساب"
		Regular, // "انتظام"
		AffliateAndRegular //"انتساب وانتظام"
	}

	public class Faculity
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string Order { get; set; }
		public FaculityType Type { get; set; }
		public virtual ICollection<Faculity_Phases>? Phases { get;  } = new List<Faculity_Phases>();
		public virtual ICollection<Faculity_Semester>? Semesters { get; } = new List<Faculity_Semester>();
		[InverseProperty("FaculityNode")]
		public virtual ICollection<Faculity_Node>? Nodes { get; } = new List<Faculity_Node>();
		[InverseProperty("FaculityEmployee")]
		public virtual ICollection<ApplicationUser> Users { get; }= new List<ApplicationUser>();
		public virtual ICollection<Control> Controls { get; }=new List<Control>();

		[ForeignKey("UserLeader")]
		public string UserLeaderID { get; set; }

		[InverseProperty("FaculityLeader")]
		public ApplicationUser UserLeader { get; set; }
	}
}
