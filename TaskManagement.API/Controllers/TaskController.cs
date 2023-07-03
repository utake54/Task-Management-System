using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Model.Model.Task.Request;
using TaskManagement.Service.TaskService;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : BaseController
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("AddTask")]
        public async Task<Dictionary<string, object>> AddTask(TaskRequest request)
        {
            var addTask = await _taskService.AddTask(UserId, request);
            if (addTask.Message == "Success")
                return APIResponse("TM200", addTask.Data);
            return FailureResponse(addTask.Message, addTask.Errors);
        }

        [HttpPost("AddTask/{taskId}")]
        public async Task<Dictionary<string, object>> GetTask(int taskId)
        {
            var task = await _taskService.GetTask(taskId);
            if (task.Message == "Success")
                return APIResponse("TM200", task.Data);
            return FailureResponse(task.Message, task.Errors);
        }

        [HttpPost("UpdateTask")]
        public async Task<Dictionary<string, object>> UpdateTask(TaskRequest request)
        {
            var updateTask = await _taskService.UpdateTask(request, UserId);
            if (updateTask.Message == "Success")
                return APIResponse("TM200", updateTask.Data);
            return FailureResponse(updateTask.Message, updateTask.Errors);
        }

        [HttpPost("DeleteTask/{taskId}")]
        public async Task<Dictionary<string, object>> DeleteTask(int taskId)
        {
            var deleteTask = await _taskService.DeleteTask(taskId);
            if (deleteTask.Message == "Success")
                return APIResponse("TM200", deleteTask.Data);
            return FailureResponse(deleteTask.Message, deleteTask.Errors);
        }

        [HttpPost("GetAllTask")]
        public async Task<Dictionary<string, object>> GetAllTask()
        {
            var allTask = await _taskService.GetAllTask(CompanyId);
            if (allTask.Message == "Success")
                return APIResponse("TM200", allTask.Data);
            return FailureResponse(allTask.Message, allTask.Errors);
        }
    }
}
