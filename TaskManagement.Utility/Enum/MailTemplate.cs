using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Utility.Enum
{
    public enum MailTemplate
    {
        None = 0,
        Welcome,
        TaskAssigned,
        TaskOverdue,
        DueDateReminder
    }
}
