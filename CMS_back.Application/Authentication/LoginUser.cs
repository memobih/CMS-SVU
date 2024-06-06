using System.ComponentModel.DataAnnotations;

namespace CMS_back.Authentication
{
    public class LoginUser
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
