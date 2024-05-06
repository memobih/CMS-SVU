using CMS_back.Models;
using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class ControlTaskResultDTO
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public ApplicationUser user { get; set; }
    }
}
