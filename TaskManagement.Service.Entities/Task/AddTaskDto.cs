using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Service.Entities.Task
{
    public class AddTaskDto
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int CompanyId { get; set; }
        public int CreatedBy { get; set; }
    }

    public class UpdateTaskDto:GetTaskDto
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int ActionBy { get; set; }
    }

    public class DeleteTaskDto : GetTaskDto
    {
        public int ActionBy { get; set; }
    }
    public class GetTaskDto
    {
        public int Id { get; set; }
    }
}
