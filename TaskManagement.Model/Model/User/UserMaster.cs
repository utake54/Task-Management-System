using System.ComponentModel.DataAnnotations.Schema;
using TaskManagement.Model.Model.CommonModel;

namespace TaskManagement.Model.Model.User
{
    [Table("UserMaster")]
    public class UserMaster : StandardFields
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public int ZipCode { get; set; }
        public int CompanyId { get; set; }
        public int RoleId { get; set; }
    }
}
