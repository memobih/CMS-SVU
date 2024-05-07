using CMS_back.Models;
using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class subjectResultDTO
    {
        [Required]
        public string id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public int? Credit_Hours { get; set; }
        public Question? IsDone { get; set; }
        public Question? IsReview { get; set; }
    }
}
