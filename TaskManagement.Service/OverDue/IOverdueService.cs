using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.Email;

namespace TaskManagement.Service.OverDueService
{
    public interface IOverdueService
    {
        Task<MailRequest> TaskOverdue();
    }
}
