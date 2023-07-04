using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Utility.Email
{
    public class SendEmail : ISendMail
    {
        private readonly IAppSettings _appSetting;
        public SendEmail(IAppSettings appSetting)
        {
            _appSetting = appSetting;
        }
        public Task SendEmailAsync(string to, string cc, string subject, string message)
        {
            Execute(to, cc, subject, message).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string to, string cc, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_appSetting.MailSetting.FromEmail, _appSetting.MailSetting.DisplayName)
                };
                to.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(t => mail.To.Add(new MailAddress(t)));
                //cc.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(c => mail.CC.Add(new MailAddress(c)));
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_appSetting.MailSetting.Host, _appSetting.MailSetting.Port))
                {
                    smtp.Credentials = new NetworkCredential(_appSetting.MailSetting.FromEmail, _appSetting.MailSetting.Password);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                var error = ex;
            }
        }
    }
}
