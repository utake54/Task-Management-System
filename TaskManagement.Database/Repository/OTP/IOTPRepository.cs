using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.OTP;
using TaskManagement.Service.Entities.Login;

namespace TaskManagement.Database.Repository.OTP
{
    public interface IOTPRepository : IRepository<OTPMaster>
    {
        Task<OTPMaster> IsValidOTP(OTPValidateDto request);
    }
}
