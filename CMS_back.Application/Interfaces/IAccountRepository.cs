using CMS_back.Authentication;
using CMS_back.DTO;
using Microsoft.AspNetCore.Identity;

namespace CMS_back.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterUserDto userDto);
        Task<LoginResult> SignInAsync(LoginUserDto loginUser);
        Task<bool> ForgetPasswordAsync(string email);
        Task<IdentityResult> ResetPasswordAsync(ResetPassword resetPassword);
        Task<IdentityResult> ChangePasswordAsync(ChangePassword changePassword);
        Task<IdentityResult> VerifyOTPAsync(VerifyOTPRequest request);
        Task<IdentityResult> SendOTPAsync(string email);
    }
}
