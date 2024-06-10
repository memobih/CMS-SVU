using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public  List<string> SubjectsIds { get; set; }
        [NotMapped]
        public  List<string>? UsersIds { get; set; }
    }
}
