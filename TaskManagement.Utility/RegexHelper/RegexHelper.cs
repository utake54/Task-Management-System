using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Utility.RegexHelper
{
    public static class RegexHelper
    {
        public const string REGEX_NAME = "^[a-z A-Z']+$";
        public const string REGEX_MOBILE_NUMBER = @"^[0-9]{10}$";
        public const string REGEX_COUNTRY_CODE = @"^\+[0-9]{1,5}$";
        public const string REGEX_PASSWORD = "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@$!%*?&])([a-zA-Z0-9@$!%*?&]{8,})$";
    }
}
