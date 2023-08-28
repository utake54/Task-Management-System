using System.ComponentModel.DataAnnotations;
using TaskManagement.Utility.RegexHelper;

namespace TaskManagement.API.Request
{
    public class LoginRequest
    {
        public string EmailOrMobile { get; set; }
        public string Password { get; set; }
    }
    public class ForgetPassswordRequest
    {
        public string EmailOrMobile { get; set; }
        public string DateOfBirth { get; set; }
    }
    public class PasswordResetRequest
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class OTPValidateRequest
    {
        public int UserId { get; set; }
        public int OTP { get; set; }
    }
}
