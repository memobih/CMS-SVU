using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CMS_back.DTO;
using CMS_back.Data;
using CMS_back.Models;
using CMS_back.Consts;
using System.Net.Mail;
using CMS_back.Mailing;
using Microsoft.AspNetCore.Authorization;
using CMS_back.Services;
using AutoMapper;
using CMS_back.Interfaces;
using CMS_back.Authentication;


namespace CMS_back.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly IMailingService _mailingService;
        private readonly IUserRepository _repoUser;
        private readonly IAccountRepository _accountRepository;

        public CMSContext context { get; }
        public UserManager<ApplicationUser> _usermanager { get; }
        public IHttpContextAccessor _contextAccessor { get; }
        public IMapper Mapper { get; }

        public AccountController(IConfiguration config, IAccountRepository accountRepository, IMailingService mailingService, IMapper mapper, IUserRepository repo)
        {
            this.config = config;
            _mailingService = mailingService;
            Mapper = mapper;
            _repoUser = repo;
            _accountRepository = accountRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> signin(LoginUserDto userDto)
        {
            var result = await _accountRepository.SignInAsync(userDto);
            return Ok(result);
        }

        [HttpPost("register")]//account/register
        public async Task<IActionResult> Registration(RegisterUserDto userDto)
        {
            var result = await _accountRepository.RegisterAsync(userDto);
            return Ok(result);
        }









        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {
            var result = await _accountRepository.ChangePasswordAsync(changePassword);
            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var result = await _accountRepository.ForgetPasswordAsync(email);
            if (result)
                return Ok();

            return BadRequest("User not found");
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var result = await _accountRepository.ResetPasswordAsync(resetPassword);
            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOTP(string email)
        {
            var result = await _accountRepository.SendOTPAsync(email);
            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOTPRequest request)
        {
            var result = await _accountRepository.VerifyOTPAsync(request);
            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully");
            }
            return BadRequest(result.Errors);
        }
    }
}
