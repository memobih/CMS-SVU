namespace Data_Access_Layer.Reposatory.Entities
{
	public enum Gender
	{
		Male,
		Female
	}
	public enum StudentStatus
	{
		Remaining,
		Freshman,
		International
	}
	public class Student
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public DateOnly DateOfBirth { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Address { get; set; }
		public string NationalID { get; set; }
		public int CityID { get; set; }
		public Gender Gender { get; set; }
		public StudentStatus Status { get; set; }
		public virtual ICollection<StudentSubject> StudentSubjects { get; } = [];

	}
}
