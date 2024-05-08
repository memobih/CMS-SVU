using CMS_back.Models;
using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class ControlUserDTO
    {
        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public string JobType { get; set; }
    }
}
