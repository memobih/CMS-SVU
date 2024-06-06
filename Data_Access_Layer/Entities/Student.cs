using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Entities
{
	public enum Gender
	{
		Male,
		Female
	}

	public class Student
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }
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
