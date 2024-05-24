﻿using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class ContorlAndItsUserJobTitleDTO
    {
        [Required]
        public string JobType { get; set; }
        [Required]
        public  UserResultDto User { get; set; }
    }
}
