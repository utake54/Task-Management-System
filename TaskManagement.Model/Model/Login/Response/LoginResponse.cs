using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.User;

namespace TaskManagement.Model.Model.Login.Response
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public UserMaster User { get; set; }
    }
}
