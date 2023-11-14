using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.User;

namespace TaskManagement.Model.Model.OTP
{
    [Table("OTPMaster")]
    public class OTPMaster
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OTP { get; set; }
        public DateTime GeneratedTime { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("UserId")]
        public UserMaster UserMaster { get; set; }
    }
}
