using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Service.Entities.Login
{
    public class LoginDto
    {
        public string EmailOrMobile { get; set; }
        public string Password { get; set; }
    }
    public class ForgetPasswordDto
    {
        public string EmailOrMobile { get; set; }
        public string DateOfBirth { get; set; }
    }
    public class PasswordResetDto
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class OTPValidateDto
    {
        public int UserId { get; set; }
        public int OTP { get; set; }
    }
}
