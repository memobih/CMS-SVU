using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class controlTaskDTO
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public List<string> UserTaskIds { get; set; }
    }
}
