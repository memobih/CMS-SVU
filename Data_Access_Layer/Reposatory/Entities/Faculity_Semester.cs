﻿namespace Data_Access_Layer.Reposatory.Entities
{
	public class Faculity_Semester
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string Order {  get; set; }
		
		public int FaculityID { get; set; }
		public Faculity Faculity { get; set; }

		public virtual ICollection<Subject>? Subjects { get; } = new List<Subject>();

	}
}
