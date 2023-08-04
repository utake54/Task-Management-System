using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Model.Model.OTP
{
    public class OTPValidateRequest
    {
        [Required(ErrorMessage ="Please select valid user.")]
        public int UserId { get; set; }

        [Required(ErrorMessage ="Please enter valid OTP.")]
        public int OTP { get; set; }
    }
}
