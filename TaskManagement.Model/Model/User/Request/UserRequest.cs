using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Utility.RegexHelper;

namespace TaskManagement.Model.Model.User.Request
{
    public class UserRequest
    {
        public int Id { get; set; }

        [RegularExpression(RegexHelper.REGEX_NAME, ErrorMessage = "Please enter valid name")]
        public string FirstName { get; set; }

        [RegularExpression(RegexHelper.REGEX_NAME, ErrorMessage = "Please enter valid name")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Please enter valid email")]
        public string EmailId { get; set; }

        [RegularExpression(RegexHelper.REGEX_MOBILE_NUMBER, ErrorMessage = "Please enter mobile number")]
        public string MobileNo { get; set; }

        //[MinLength(8, ErrorMessage = "Password must be minimun 8 digits.")]
        //[RegularExpression(RegexHelper.REGEX_PASSWORD, ErrorMessage = "Password must contain special character,number and letter.")]
        //public string Password { get; set; }

        [Required(ErrorMessage = "Date of birth required.")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter city name")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter state name")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter country name")]
        public string Country { get; set; }
        [RegularExpression(RegexHelper.REGEX_COUNTRY_CODE, ErrorMessage = "Please enter valid country code")]
        public string CountryCode { get; set; }
        public int ZipCode { get; set; }
    }
}
