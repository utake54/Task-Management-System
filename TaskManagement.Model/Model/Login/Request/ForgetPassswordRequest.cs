using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Model.Model.Login.Request
{
    public class ForgetPassswordRequest
    {
        [Required(ErrorMessage ="Please enter Email or Mobile number.")]
        public string EmailOrMobile { get; set; }

        [Required(ErrorMessage ="Please select date of birth.")]
        public string DateOfBirth { get; set; }
    }
}
