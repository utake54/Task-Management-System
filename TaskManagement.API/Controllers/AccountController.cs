using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Infrastructure;
using TaskManagement.Model.Model.Login.Request;
using TaskManagement.Model.Model.OTP;
using TaskManagement.Service.OTPService;
using TaskManagement.Service.UserService;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IOTPService _otpService;
        public AccountController(IUserService userService, IOTPService oTPService)
        {
            _userService = userService;
            _otpService = oTPService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userService.Login(request);
            if (user != null)
            {
                var _token = JWTHelper.Login(user);
                return Ok(_token);
            }
            return Unauthorized(); ;
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
            return FailureResponse("Invalid details", null);
        }

        [HttpPost("ValidateOTP")]
        public async Task<Dictionary<string, object>> ValidateOtp(OTPValidateRequest request)
        {
            var validateOtp = await _otpService.ValidateOTP(request);
            if (validateOtp.Message == "Success")
            {
                return APIResponse("OTP validate successfully", null);
            }
            return FailureResponse(validateOtp.Message, validateOtp.Data);
        }

        [HttpPost("ResetPassword")]
        public async Task<Dictionary<string, object>> ResetPassword(PasswordResetRequest request)
        {
            var changedPassword = await _userService.ResetPassword(request);
            if (changedPassword.Message == "Success")
            {
                return APIResponse("Password changed successfully", null);
            }
            return FailureResponse(changedPassword.Message, changedPassword.Data);
        }
    }
}
