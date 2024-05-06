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
    }
}
