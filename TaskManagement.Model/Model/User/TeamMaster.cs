using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.Company;

namespace TaskManagement.Model.Model.User
{
    [Table("TeamMaster")]
    public class TeamMaster
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ParentId { get; set; }
        public int CompanyId { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("CompanyId")]
        public CompanyMaster? Company { get; set; }

        [ForeignKey("UserId")]
        public UserMaster? UserMaster { get; set; }
    }
}
