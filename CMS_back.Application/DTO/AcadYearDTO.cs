using System.ComponentModel.DataAnnotations;

namespace CMS_back.Application.DTO
{
    public class AcadYearDTO
    {
        [RegularExpression(pattern: @"^\d{4}\/\d{4}$", ErrorMessage = "Invalid ACAD_YEAR Format. Please use the format 'YYYY/YYYY'.\"")]
        public string Name { get; set; }
    }
}
