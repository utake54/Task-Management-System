using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Model.Model.Task.Request
{
    public class AssignTask
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AssignedBy { get; set; }
        public int? Status { get; set; }
        public bool? IsAcceptByUser { get; set; }
    }

    public class AssignTaskRequest
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int[] UserId { get; set; }
    }
}
