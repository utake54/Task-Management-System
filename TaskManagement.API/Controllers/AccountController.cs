using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.API.Request;
using TaskManagement.Service.Entities.Login;
using TaskManagement.Service.OTPService;
using TaskManagement.Service.UserService;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IOTPService _otpService;
        private readonly IMapper _mapper;
        public AccountController(IUserService userService,
                                 IOTPService oTPService,
                                 IMapper mapper)
        {
            _userService = userService;
            _otpService = oTPService;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<Dictionary<string, object>> LoginAsync(LoginRequest request)
        {
            var requestDto = _mapper.Map<LoginDto>(request);
            var result = await _userService.Login(requestDto);
            return NewAPIResponse(result.Data, result.Message);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok("TM011");
        }

        [HttpPost("ForgetPassword")]
        public async Task<Dictionary<string, object>> ForgetPassword(ForgetPassswordRequest request)
        {
            var requestDto = _mapper.Map<ForgetPasswordDto>(request);
            var userData = await _userService.ForgetPassword(requestDto);
            return NewAPIResponse(userData.Data, userData.Message);
        }

        [HttpPost("ValidateOTP")]
        public async Task<Dictionary<string, object>> ValidateOtp(OTPValidateRequest request)
        {
            var requestDto = _mapper.Map<OTPValidateDto>(request);
            var validateOtp = await _otpService.ValidateOTP(requestDto);
            return NewAPIResponse(validateOtp.Data, validateOtp.Message);
        }

        [HttpPost("ResetPassword")]
        public async Task<Dictionary<string, object>> ResetPassword(PasswordResetRequest request)
        {
            var requestDto = _mapper.Map<PasswordResetDto>(request);
            var changedPassword = await _userService.ResetPassword(requestDto);
            return NewAPIResponse(changedPassword.Result, changedPassword.Message, "User deleted successfully.");
        }
    }
}