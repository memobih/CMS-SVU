﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Entities
{
	//public enum JobType
	//{
	//	Member,
	//	Head
	//}
	public class ControlUsers
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string ControlID { get; set; }
		[ForeignKey(nameof(ControlID))]
		public Control Control {  get; set; }
		public string? UserID { get; set; }
		public ApplicationUser? User { get; set; }
		
		public string JobType { get; set;}
	}
}
