using AutoMapper;
using CMS_back.Application.Helpers;
using CMS_back.Authentication;
using CMS_back.Consts;
using CMS_back.DTO;
using CMS_back.Interfaces;
using CMS_back.Mailing;
using CMS_back.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CMS_back.Services
{
    public class AccountRepository : IAccountRepository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IMailingService _mailingService;
        private readonly IConfiguration _config;
        private readonly IUserHelpers _userHelper;

        public AccountRepository(UserManager<ApplicationUser> userManager, IMapper mapper,
            IMailingService mailingService, IConfiguration config, IUserHelpers userHelpers)
        {
            _userManager = userManager;
            _mapper = mapper;
            _mailingService = mailingService;
            _config = config;
            _userHelper = userHelpers;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterUserDto userDto)
        {

            var currentUser = await _userHelper.GetCurrentUserAsync();
            ApplicationUser userResult = _mapper.Map<ApplicationUser>(userDto);
            userResult.Type = ConstsRoles.Staff;
            if (currentUser != null)
            {
                userResult.FaculityEmployeeID = currentUser.FaculityLeaderID;
            }
            var userExist = await _userManager.FindByNameAsync(userDto.UserName);

            if (userExist != null)
            {
                throw new Exception("User Already Exist");
            }

            IdentityResult result = await _userManager.CreateAsync(userResult, userDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userResult, userResult.Type);
                userResult.EmailConfirmed = true;
                await _userManager.UpdateAsync(userResult);

                var message = new MailMessage(new string[] { userResult.Email }, "register", $"Hi {userResult.Name}, You have Registered Successfully In CMS(Control Management System)");
                _mailingService.SendMail(message);
                return result;
            }
            return IdentityResult.Failed(new IdentityError { Description = "not allowed to add users" });
        }

        public async Task<LoginResult> SignInAsync(LoginUserDto loginUser)
        {
            var user = await _userManager.FindByNameAsync(loginUser.UserName);
            if (user == null)
            {
                return new LoginResult
                {
                    Success = false,
                    Token = null,
                    Expiration = default,
                    ErrorType = LoginErrorType.UserNotFound
                };
            }

            if (!await _userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                return new LoginResult
                {
                    Success = false,
                    Token = null,
                    Expiration = default,
                    ErrorType = LoginErrorType.InvalidPassword
                };
            }
            if (!user.EmailConfirmed)
            {
                return new LoginResult
                {
                    Success = false,
                    Token = null,
                    Expiration = default,
                    ErrorType = LoginErrorType.EmailNotConfirmed
                };
            }
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Role, user.Type));

            if (user.FaculityLeaderID != null)
                claims.Add(new Claim(ClaimTypes.Sid, user.FaculityLeaderID));

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            SecurityKey securityKey =
               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

            SigningCredentials signincred =
                        new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create token
            JwtSecurityToken mytoken = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signincred
                );

            return new LoginResult
            {
                Success = true,
                Token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                Expiration = mytoken.ValidTo,
                roles = user.Type,

            };
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePassword changePassword)
        {
            var currentUser = await _userHelper.GetCurrentUserAsync();
            if (currentUser == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not Found" });

            if (currentUser.OTP != changePassword.OTP || currentUser.OTPExpiry < DateTime.UtcNow)
                return IdentityResult.Failed(new IdentityError { Description = "Invalid Or Expired OTP" });

            currentUser.OTP = null;
            currentUser.OTPExpiry = DateTime.MinValue;

            var result = await _userManager.ChangePasswordAsync(currentUser, changePassword.CurrentPassword, changePassword.NewPassword);
            await _userManager.UpdateAsync(currentUser);

            return result;
        }

        public async Task<bool> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;

            await SendOTPAsync(email);
            return true;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User Not Found" });

            if (user.OTP != resetPassword.OTP || user.OTPExpiry < DateTime.UtcNow)
                return IdentityResult.Failed(new IdentityError { Description = "Invalid Or Expired OTP" });

            user.OTP = null;
            user.OTPExpiry = DateTime.MinValue;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword);
            await _userManager.UpdateAsync(user);

            return result;
        }

        public async Task<IdentityResult> SendOTPAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User Not Found" });

            var otp = GenerateOTP();
            user.OTP = otp;
            user.OTPExpiry = DateTime.UtcNow.AddMinutes(15);
            await _userManager.UpdateAsync(user);

            var message = new MailMessage(new[] { user.Email }, "Your OTP", $"Your OTP For Change Your Password In CMS is: {otp}");
            _mailingService.SendMail(message);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> VerifyOTPAsync(VerifyOTPRequest verifyOTPRequest)
        {
            var user = await _userManager.FindByEmailAsync(verifyOTPRequest.Email);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User Not Found" });

            if (user.OTP != verifyOTPRequest.OTP || user.OTPExpiry < DateTime.UtcNow)
                return IdentityResult.Failed(new IdentityError { Description = "Invalid Or Expired OTP" });

            user.OTP = string.Empty;
            user.OTPExpiry = DateTime.MinValue;
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);

            return IdentityResult.Success;
        }

        private string GenerateOTP()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var byteArray = new byte[6];
                rng.GetBytes(byteArray);

                var sb = new StringBuilder();
                foreach (var byteValue in byteArray)
                {
                    sb.Append(byteValue % 10);
                }
                return sb.ToString();
            }
        }

    }
}
