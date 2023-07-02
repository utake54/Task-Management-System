using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Model.Model.OTP
{
    public class OTPMaster
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OTP { get; set; }
        public DateTime GeneratedTime { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
