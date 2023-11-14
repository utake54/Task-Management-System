using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Model.Model.User
{
    [Table("RoleMaster")]
    public class RoleMaster
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }

        [Column(TypeName = "varchar")]
        public string Role { get; set; }

        public ICollection<UserMaster> UserMasters { get; set; }
    }
}
