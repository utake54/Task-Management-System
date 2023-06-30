using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Utility.RegexHelper;

namespace TaskManagement.Model.Model.Login.Request
{
    public class PasswordResetRequest
    {
        public int UserId { get; set; }
        [RegularExpression(RegexHelper.REGEX_PASSWORD)]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
