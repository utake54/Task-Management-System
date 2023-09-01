using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Request;
using TaskManagement.Service.Entities.Login;
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
            if (result.Data != null)
            {
                return APIResponse("TM009", result.Data);
            }
            return UnauthorizeResponse("TM010", result.Message);
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
            if (userData.Data != null)
            {
                return APIResponse("TM012", userData.Data);
            }
            return FailureResponse("TM013", userData.Message);
        }

        [HttpPost("ValidateOTP")]
        public async Task<Dictionary<string, object>> ValidateOtp(OTPValidateRequest request)
        {
            var requestDto = _mapper.Map<OTPValidateDto>(request);
            var validateOtp = await _otpService.ValidateOTP(requestDto);
            if (validateOtp.Message == "Success")
            {
                return APIResponse("Success", null);
            }
            return FailureResponse("TM015", validateOtp.Message);
        }

        [HttpPost("ResetPassword")]
        public async Task<Dictionary<string, object>> ResetPassword(PasswordResetRequest request)
        {
            var requestDto = _mapper.Map<PasswordResetDto>(request);
            var changedPassword = await _userService.ResetPassword(requestDto);
            if (changedPassword.Message == "Success")
            {
                return APIResponse("TM016", null);
            }
            return FailureResponse("TM017", changedPassword.Message);
        }
    }
}
