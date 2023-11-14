using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagement.Model.Model.CommonModel;
using TaskManagement.Model.Model.Company;

namespace TaskManagement.Model.Model.User
{
    [Table("UserMaster")]
    public class UserMaster : StandardFields
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public int ZipCode { get; set; }
        public int CompanyId { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("CompanyId")]
        public CompanyMaster? CompanyMaster { get; set; }

        [ForeignKey("RoleId")]
        public RoleMaster? RoleMaster { get; set; }
    }
}
