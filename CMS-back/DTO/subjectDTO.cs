using CMS_back.Reposatory.Models;
using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class subjectDTO
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Code { get; set; }
        
        [Required]
        public int Credit_Hours { get; set; }
        
        [Required]
        public string? FaculitySemesterID { get; set; }
        
        [Required]
        public string? FaculityPhaseID { get; set; }
    }
}
