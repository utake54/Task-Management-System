using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.Email;
using TaskManagement.Utility.Email;

namespace TaskManagement.Service.OverDueService
{
    public class OverdueService : IOverdueService
    {
        public readonly ISendMail _sendMail;
        public OverdueService(ISendMail sendMail)
        {
            _sendMail = sendMail;
        }
        public async Task<MailRequest> TaskOverdue()
        {
            MailRequest mailRequestmail = new MailRequest();
            mailRequestmail.Subject = "TestHanfFireSchedule";
            mailRequestmail.Body = "New test for job";
            mailRequestmail.ToEmail = "omkar.utake54@gmail.com";


            await _sendMail.SendEmailAsync(mailRequestmail.ToEmail, null, mailRequestmail.Subject, "mailRequestmail.Message");

            return mailRequestmail;
        }
    }
}
