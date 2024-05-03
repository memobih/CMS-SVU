using System.ComponentModel.DataAnnotations.Schema;

namespace Business_Logic_Layer.Models
{
	public enum FaculityType
	{
		Affliate, // "انتساب"
		Regular, // "انتظام"
		AffliateAndRegular //"انتساب وانتظام"
	}

	public class FaculityModel
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string Order { get; set; }
		public FaculityType Type { get; set; }
		public virtual ICollection<Faculity_PhasesModel>? Phases { get;  } = new List<Faculity_PhasesModel>();
		public virtual ICollection<Faculity_SemesterModel>? Semesters { get; } = new List<Faculity_SemesterModel>();
		[InverseProperty("FaculityNode")]
		public virtual ICollection<Faculity_NodeModel>? Nodes { get; } = new List<Faculity_NodeModel>();
		[InverseProperty("FaculityEmployee")]
		public virtual ICollection<ApplicationUserModel> Users { get; }= new List<ApplicationUserModel>();
		public virtual ICollection<ControlModel> Controls { get; }=new List<ControlModel>();

		[ForeignKey("UserLeader")]
		public string UserLeaderID { get; set; }

		[InverseProperty("FaculityLeader")]
		public ApplicationUserModel UserLeader { get; set; }
	}
}
