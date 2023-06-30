using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Infrastructure;
using TaskManagement.Model.Model.Login.Request;
using TaskManagement.Service.UserService;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
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
            if (userData != null)
            {
                return APIResponse("Please enter new password", userData.Data);
            }

            return FailureResponse("Invalid details", null);
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
