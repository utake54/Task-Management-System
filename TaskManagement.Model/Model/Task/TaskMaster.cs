using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.CommonModel;

namespace TaskManagement.Model.Model.Task
{
    public class TaskMaster : StandardFields
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int CompanyId { get; set; }
    }
}
