namespace TaskManagement.API.Request
{
    public class AddTaskRequest
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime DueDate { get; set; }
    }
    public class GetTaskByIdRequest
    {
        public int Id { get; set; }
    }

    public class UpdateTaskRequest : AddTaskRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTaskRequest : GetTaskByIdRequest
    {
    }

    public class AcceptTaskRequest
    {
        public int TaskId { get; set; }
        public bool IsAccepted { get; set; }
    }

    public class AssignTaskRequest
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int[] UserId { get; set; }
    }

    public class TaskStatusRequest
    {
        public int TaskId { get; set; }
        public int StatusId { get; set; }
    }
}
