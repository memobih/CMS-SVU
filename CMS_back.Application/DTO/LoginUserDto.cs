using System.ComponentModel.DataAnnotations;

namespace CMS_back.DTO
{
    public class LoginUserDto
    {

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}