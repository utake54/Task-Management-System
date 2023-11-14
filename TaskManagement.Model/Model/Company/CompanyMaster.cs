
using System.ComponentModel.DataAnnotations;
using TaskManagement.Model.Model.User;

namespace TaskManagement.Model.Model.Company
{
    public class CompanyMaster
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserMaster> UserMasters { get; set; }    
    }
}
