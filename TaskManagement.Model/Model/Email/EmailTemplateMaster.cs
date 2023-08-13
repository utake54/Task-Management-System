using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Model.Model.Email
{
    [Table("EmailTemplateMaster")]
    public class EmailTemplateMaster
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
