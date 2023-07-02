using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Model.Model.OTP
{
    public class OTPValidateRequest
    {
        public int UserId { get; set; }
        public int OTP { get; set; }
    }
}
