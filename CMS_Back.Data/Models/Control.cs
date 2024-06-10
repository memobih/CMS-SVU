using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_back.Models
{
	public class Control
	{

		[Key,MaxLength(200)]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string Name { get; set; }
		public string? Faculity_Phase { get; set; }
		public string? Faculity_Node { get; set; }
		public string? Faculity_Semester { get; set; }

        //[Column(TypeName = "Date")]
        public DateOnly? Start_Date { get; set; }
        //[Column(TypeName = "Date")]
        public DateOnly? End_Date { get; set; }

		[RegularExpression(pattern: @"^\d{4}\/\d{4}$",ErrorMessage = "Invalid ACAD_YEAR Format. Please use the format 'YYYY/YYYY'.\"")]
		public string ACAD_YEAR { get; set; }

		public virtual  ICollection<Control_Addresses>? conrol_Addresses  { get; set; }

		[ForeignKey("UserCreator")]
		public string UserCreatorID { get; set; }
		public virtual ApplicationUser UserCreator { get; set; }

		public string FaculityID { get; set; }
		public Faculity Faculity { get; set; }

		public virtual ICollection<ControlSubjects> ControlSubjects { get; set; } 
		public virtual ICollection<ControlUsers>? ControlUsers { get; set; }
        public virtual ICollection<Control_Note>? Control_Notes { get; set; } 
		public virtual ICollection<Control_Task>? Control_Tasks { get; set; }
    }
}
