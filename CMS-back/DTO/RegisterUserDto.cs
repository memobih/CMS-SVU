using CMS_back.Models;
using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class RegisterUserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string ScientificDegree { get; set; }

        [Required]
        [Range(1,4)]
        public UserType Type { get; set; }

        public string? FaculityID { get; set; }
    }
}