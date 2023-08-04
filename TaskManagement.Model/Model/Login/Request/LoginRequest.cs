using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Utility.RegexHelper;

namespace TaskManagement.Model.Model.Login.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Please enter email or mobile number.")]
        public string EmailOrMobile { get; set; }

        [RegularExpression(RegexHelper.REGEX_PASSWORD,ErrorMessage ="Please enter valid password.")]
        public string Password { get; set; }
    }
}
