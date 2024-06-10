using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS_back.Models
{
	public enum Gender
	{
		Male,
		Female
	}

	public class Student
	{
		[Key, MaxLength(200)]		
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }

        //[Column(TypeName = "Date")]
        public DateOnly? DateOfBirth { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Address { get; set; }
		public string? NationalID { get; set; }
		public int? CityID { get; set; }
		public Gender? Gender { get; set; }

		public string? FaculityID { get; set; }
		public Faculity? Faculity { get; set; }

		public virtual ICollection<StudentSemester>? Semesters { get; set; }	


	}
}
