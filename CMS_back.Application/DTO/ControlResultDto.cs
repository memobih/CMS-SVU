using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class ControlResultDto
    {
        public string Id { get; set; } 
        public string Name { get; set; }
        public string? Faculity_Phase { get; set; }
        public string? Faculity_Node { get; set; }
        public string? Faculity_Semester { get; set; }
        public DateOnly? Start_Date { get; set; }
        public DateOnly? End_Date { get; set; }

        [RegularExpression(pattern: @"^\d{4}\/\d{4}$",
            ErrorMessage = "Invalid ACAD_YEAR Format. Please use the format 'YYYY/YYYY'.\"")]
        public string? ACAD_YEAR { get; set; }

        public LeaderResultDto UserCreator { get; set; }
    }
}
