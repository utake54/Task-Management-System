using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Utility
{
    public static class OTPGenerator
    {
        public static int GetOTP()
        {
            var otp = new Random();
            var randomOTP = otp.Next(1000, 9999);
            return randomOTP;
        }
    }
}
