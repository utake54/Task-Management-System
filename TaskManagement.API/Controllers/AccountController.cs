using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Infrastructure;
using TaskManagement.Model.Model.Login.Request;
using TaskManagement.Model.Model.OTP;
using TaskManagement.Model.Model.User;
using TaskManagement.Service.OTPService;
using TaskManagement.Service.UserService;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IOTPService _otpService;
        private readonly IMapper _mapper;
        public AccountController(IUserService userService, IOTPService oTPService, IMapper mapper)
        {
            _userService = userService;
            _otpService = oTPService;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<Dictionary<string, object>> Login(LoginRequest request)
        {
            var user = await _userService.Login(request);
            if (user.Data != null)
            {
                var _token = JWTHelper.Login((UserMaster)user.Data);
                return APIResponse("Success", _token);
            }
            return UnauthorizeResponse("Unauthorized", user.Message);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok("Log out success");
        }

        [HttpPost("ForgetPassword")]
        public async Task<Dictionary<string, object>> ForgetPassword(ForgetPassswordRequest request)
        {
            var userData = await _userService.ForgetPassword(request);
            if (userData.Data != null)
            {
                return APIResponse("OTP sent successfully on email", userData.Data);
            }
            return FailureResponse("Invalid details", userData.Message);
        }

        [HttpPost("ValidateOTP")]
        public async Task<Dictionary<string, object>> ValidateOtp(OTPValidateRequest request)
        {
            var validateOtp = await _otpService.ValidateOTP(request);
            if (validateOtp.Message == "Success")
            {
                return APIResponse("OTP validate successfully", null);
            }
            return FailureResponse("Invalid OTP", validateOtp.Message);
        }

        [HttpPost("ResetPassword")]
        public async Task<Dictionary<string, object>> ResetPassword(PasswordResetRequest request)
        {
            var changedPassword = await _userService.ResetPassword(request);
            if (changedPassword.Message == "Success")
            {
                return APIResponse("Password changed successfully", null);
            }
            return FailureResponse("Failed to update password", changedPassword.Message);
        }
    }
}
