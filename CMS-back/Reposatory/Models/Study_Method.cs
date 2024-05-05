﻿using System.ComponentModel.DataAnnotations;

namespace CMS_back.Reposatory.Models
{
	public class Study_Method
	{
		[Key]
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string? Name { get; set; }
	}
}
