using Microsoft.AspNetCore.Mvc;
using CMS_back.DTO;
using CMS_back.Interfaces;
using CMS_back.Authentication;


namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {

        private readonly IAccountRepository _accountRepository;

        public AccountController( IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> signin(LoginUserDto userDto)
        {
            var result = await _accountRepository.SignInAsync(userDto);
            if (result == null) return BadRequest("Invalid login Credentials. Please check your username and password and try again.");
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration(RegisterUserDto userDto)
        {
            var result = await _accountRepository.RegisterAsync(userDto);
            if (result == null) return BadRequest("There Was a Problem With This Registration. Please Check The Information You Entered and Try Again.");
            return Ok("Registeration Successfully");
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {
            var result = await _accountRepository.ChangePasswordAsync(changePassword);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Ok("Your Password Has been Successfully Changed");         
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var result = await _accountRepository.ForgetPasswordAsync(email);
            return result ? Ok("A password Reset link has been sent to your email Address") : BadRequest("User Not Found To Send OTP for Change Password");
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var result = await _accountRepository.ResetPasswordAsync(resetPassword);
            if (!result.Succeeded) return BadRequest(result.Errors); 
            return Ok("Your Password Has been Successfully Reset. You Can Now Login Using Your New Password");
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOTP(string email)
        {
            var result = await _accountRepository.SendOTPAsync(email);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Ok("A One-Time Passcode (OTP) Has been Sent To Your Email. Please Enter The Code To Continue");
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOTP([FromBody] VerifyOTPRequest request)
        {
            var result = await _accountRepository.VerifyOTPAsync(request);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return Ok("Email Confirmed Successfully");           
        }
    }
}
