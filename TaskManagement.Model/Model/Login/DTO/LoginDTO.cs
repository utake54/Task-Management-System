using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Model.Model.Login.DTO
{
    public class LoginDTO
    {
        public string FirstName { get; set; }
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
        public string Role { get; set; }
    }
}
