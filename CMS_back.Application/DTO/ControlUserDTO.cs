using CMS_back.Models;
using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class ControlUserDTO
    {
        public ApplicationUser User { get; set; }
        public string JobType { get; set; }
    }
}
