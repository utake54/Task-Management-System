using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Utility.Email
{
    public interface ISendMail
    {
        Task SendEmailAsync(string to, string cc, string subject, string message);
    }
}
