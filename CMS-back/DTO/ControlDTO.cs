using CMS_back.Reposatory.Models;
using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class ControlDTO
    {
        [Required]
        public string Name { get; set; }
        public string? Faculity_Phase { get; set; }
        public string? Faculity_Node { get; set; }
        [Required]
        public DateOnly Start_Date { get; set; }
        public DateOnly? End_Date { get; set; }
        [RegularExpression(pattern: @"^\d{4}\/\d{4}$", ErrorMessage = "Invalid ACAD_YEAR Format. Please use the format 'YYYY/YYYY'.\"")]
        public string ACAD_YEAR { get; set; }
        [Required]
        public string Faculity_Semester { get; set; }
        [Required]
        public string ControlManagerID { get; set; }
        [Required]
        public string ControlCreateID { get; set; }
        [Required]
        public virtual List<string> ControlSubjectsIDs { get; set; } = [];
        [Required]
        public List<string> ContorlUsersIDs { get; set; } = [];
    }
}
