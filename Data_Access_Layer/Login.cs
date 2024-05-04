using Data_Access_Layer.Reposatory.Entities;
using Microsoft.AspNetCore.Identity;

namespace Data_Access_Layer
{
    public class Login
    {
        private UserManager<ApplicationUser> usermanger { get; }
        public Login(UserManager<ApplicationUser> usermanger)
        {
            this.usermanger=usermanger;
        }
        public async Task<ApplicationUser> signin(string username,string password)
        {
            ApplicationUser user = await usermanger.FindByNameAsync(username);
            if (user == null)
            {
                bool found = await usermanger.CheckPasswordAsync(user, password);
                if (found)
                {
                    return user;
                }
                return null;
            }
            return null;
        }

    }
}
