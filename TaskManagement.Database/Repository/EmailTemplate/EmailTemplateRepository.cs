using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.Email;

namespace TaskManagement.Database.Repository.EmailTemplate
{
    public class EmailTemplateRepository : Repository<EmailTemplateMaster>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(MasterDbContext context) : base(context)
        {
        }
    }
}
