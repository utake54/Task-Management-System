using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.Email;

namespace TaskManagement.Database.Repository.EmailTemplate
{
    public interface IEmailTemplateRepository : IRepository<EmailTemplateMaster>
    {
    }
}
