using System;
using System.Collections.Generic;
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

    public class UpdateTaskDto : GetTaskByIdDto
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
        public int ActionBy { get; set; }
    }

    public class DeleteTaskDto : GetTaskByIdDto
    {
        public int ActionBy { get; set; }
    }
    public class GetTaskByIdDto
    {
        public int Id { get; set; }
    }
    public class AcceptTaskDto
    {
        public int TaskId { get; set; }
        public bool IsAccepted { get; set; }
    }

    public class AssignTaskDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int[] UserId { get; set; }
    }

    public class TaskStatusDto
    {
        public int TaskId { get; set; }
        public int StatusId { get; set; }
    }
}
