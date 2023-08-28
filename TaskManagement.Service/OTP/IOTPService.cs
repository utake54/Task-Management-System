using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.OTP;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Service.Entities.Login;

namespace TaskManagement.Service.OTPService
{
    public interface IOTPService
    {
        Task<int> AddOTP(int userId, int otp);
        Task<ResponseModel> ValidateOTP(OTPValidateDto request);
    }
}
