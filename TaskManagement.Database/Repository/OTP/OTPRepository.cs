using Microsoft.EntityFrameworkCore;
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
    public class OTPRepository : Repository<OTPMaster>, IOTPRepository
    {
        public OTPRepository(MasterDbContext context) : base(context)
        {
        }

        public async Task<OTPMaster> IsValidOTP(OTPValidateDto request)
        {
            var checkOTP = await (from o in Context.OTPMaster
                                  where o.OTP == request.OTP && o.UserId == request.UserId && o.ExpiryTime > DateTime.UtcNow
                                  select o).FirstOrDefaultAsync();
            return checkOTP;
        }
    }
}
