using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Service.Entities.ModelDto
{
    public class AddUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public int ZipCode { get; set; }
        public int CompanyId { get; set; }
        public int CreatedBy { get; set; }
    }

    public class UpdateUserDto : GetUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public int ZipCode { get; set; }
        public int ModifiedBy { get; set; }
    }
    public class GetUserDto
    {
        public int Id { get; set; }
    }

    public class DeleteUserDto
    {
        public int Id { get; set; }
        public int ActionBy { get; set; }
    }

}
