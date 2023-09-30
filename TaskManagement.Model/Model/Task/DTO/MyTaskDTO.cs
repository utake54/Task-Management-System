using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Model.Model.Task.DTO
{
    public class MyTaskDTO
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Task { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string AssignedDate { get; set; }
        public string DueDate { get; set; }
        public string CompletedDate { get; set; }
        public string AssignedBy { get; set; }


    }

    public class TaskMasterDto
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string AssignedDate { get; set; }
        public string DueDate { get; set; }
    }
}
