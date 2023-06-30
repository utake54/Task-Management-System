using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Model.Model.Login.Request
{
    public class ForgetPassswordRequest
    {
        public string EmailOrMobile { get; set; }
        public string DateOfBirth { get; set; }
    }
}
