using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.User;

namespace TaskManagement.Model.Model.Task
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

        [ForeignKey("TaskId")]
        public TaskMaster TaskMaster { get; set; }

        [ForeignKey("UserId")]
        public UserMaster UserMaster { get; set; }


    }
}
