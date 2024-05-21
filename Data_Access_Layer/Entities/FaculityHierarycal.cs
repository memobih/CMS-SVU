﻿using System.ComponentModel.DataAnnotations;

namespace Data_Access_Layer.Entities
{
	public class FaculityHierarycal
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }
		public string? Order {  get; set; }

		public string? SemesterID { get; set; }
		public Faculity_Semester? Semester { get; set; }
		public string? PhaseID { get; set; }
		public Faculity_Phases? Phase { get; set; }
		public string? BAYLAWID { get; set; }
		public BYLAW? BYLAW { get; set; }

	}
}