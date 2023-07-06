using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Model.Model.Task.Request
{
    public class TaskStatusRequest
    {
        public int TaskId { get; set; }
        public int StatusId { get; set; }
    }
}
