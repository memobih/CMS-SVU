using CMS_back.Models;
using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class UserWithHisControlDTO
    {
        [Required]
        public string role { get; set; }
        [Required]
        public ControlResultDto control { get; set; }
    }
}
