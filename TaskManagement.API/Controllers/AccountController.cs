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

        [HttpPost("LoginAsync")]
        public async Task<Dictionary<string, object>> LoginAsync(LoginRequest request)
        {
            var requestDto = _mapper.Map<LoginDto>(request);
            var result = await _userService.Login(requestDto);
            return APIResponse(result.Data, result.Message);
        }

        [HttpPost("LogoutAsync")]
        public async Task<Dictionary<string, object>> Logout()
        {
            return APIResponse(true,"TM011");
        }

        [HttpPost("ForgetPasswordAsync")]
        public async Task<Dictionary<string, object>> ForgetPassword(ForgetPassswordRequest request)
        {
            var requestDto = _mapper.Map<ForgetPasswordDto>(request);
            var userData = await _userService.ForgetPassword(requestDto);
            return APIResponse(userData.Data, userData.Message, "TM012");
        }

        [HttpPost("ValidateOTPAsync")]
        public async Task<Dictionary<string, object>> ValidateOtp(OTPValidateRequest request)
        {
            var requestDto = _mapper.Map<OTPValidateDto>(request);
            var validateOtp = await _otpService.ValidateOTP(requestDto);
            return APIResponse(validateOtp.Data, validateOtp.Message, "TM014");
        }

        [HttpPost("ResetPasswordAsync")]
        public async Task<Dictionary<string, object>> ResetPassword(PasswordResetRequest request)
        {
            var requestDto = _mapper.Map<PasswordResetDto>(request);
            var changedPassword = await _userService.ResetPassword(requestDto);
            return APIResponse(changedPassword.Result, changedPassword.Message, "TM016");
        }
    }
}