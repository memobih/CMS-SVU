using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class RegisterUserDto
    {

        public string Name { get; set; }

        public string UserName { get; set; }


        public string Email { get; set; }

        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string ScientificDegree { get; set; }

    }
}