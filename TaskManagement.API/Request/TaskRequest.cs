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
    public class GetTaskRequest
    {
        public int Id { get; set; }
    }

    public class UpdateTaskRequest : AddTaskRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTaskRequest : GetTaskRequest
    {
    }
}
