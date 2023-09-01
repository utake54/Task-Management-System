using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.Email;
using TaskManagement.Utility.Email;
using TaskManagement.Utility.Enum;

namespace TaskManagement.Service.OverDueService
{
    public class OverdueService : IOverdueService
    {
        public readonly ISendMail _sendMail;
        private readonly IUnitOfWork _unitOfWork;
        public OverdueService(ISendMail sendMail, IUnitOfWork unitOfWork)
        {
            _sendMail = sendMail;
            _unitOfWork = unitOfWork;
        }
        public async Task<MailRequest> TaskOverdue()
        {
            var users = await _unitOfWork.AssignTaskRepository.Get(x => x.EndDate >= DateTime.Now);

            var DueDateTaskUsers = users.Select(x => x.UserId).ToList();
            var userEmails = new List<string>();
            foreach (var user in DueDateTaskUsers)
            {
                var userEmail = await _unitOfWork.UserRepository.GetDefault(x => x.Id == user);
                var mailid = userEmail.EmailId;
                userEmails.Add(mailid);
            }

            MailRequest mailRequestmail = new MailRequest();
            mailRequestmail.Subject = "TestHanfFireOvedueSchedule";
            mailRequestmail.Body = "New test for job";
            mailRequestmail.ToEmail = "omkar.utake54@gmail.com";

            var emailDetails = new MailDetails()
            {
                To = userEmails,
                CC = userEmails,
                Subject = "TestHanfFireOvedueSchedule",
                Message = "job overdue"
            };
            await _sendMail.SendEmailAsync(emailDetails);
            return mailRequestmail;
        }
    }
}
