using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Model.Model.Task.Request;
using TaskManagement.Service.TaskService;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
                return APIResponse("Success", addTask.Data);
            return FailureResponse("Failed", addTask.Message);
        }

        [HttpPost("GetTask/{taskId}")]
        public async Task<Dictionary<string, object>> GetTask(int taskId)
        {
            var task = await _taskService.GetTask(taskId);
            if (task.Message == "Success")
                return APIResponse("Success", task.Data);
            return FailureResponse("Failed", task.Message);
        }

        [HttpPost("UpdateTask")]
        public async Task<Dictionary<string, object>> UpdateTask(TaskRequest request)
        {
            var updateTask = await _taskService.UpdateTask(request, UserId);
            if (updateTask.Message == "Success")
                return APIResponse("Success", updateTask.Data);
            return FailureResponse("Failed", updateTask.Message);
        }

        [HttpPost("DeleteTask/{taskId}")]
        public async Task<Dictionary<string, object>> DeleteTask(int taskId)
        {
            var deleteTask = await _taskService.DeleteTask(taskId);
            if (deleteTask.Message == "Success")
                return APIResponse("Success", deleteTask.Data);
            return FailureResponse("Failed", deleteTask.Message);
        }

        [HttpPost("GetAllTask")]
        public async Task<Dictionary<string, object>> GetAllTask()
        {
            var allTask = await _taskService.GetAllTask(CompanyId);
            if (allTask.Message == "Success")
                return APIResponse("Success", allTask.Data);
            return FailureResponse("Failed", allTask.Message);
        }

        [HttpPost("AssignToTeam")]
        public async Task<Dictionary<string, object>> AssignTask(AssignTaskRequest request)
        {
            var assignTask = await _taskService.AssignTask(request, UserId);

            if (assignTask.Message == "Success")
                return APIResponse("Success", assignTask.Data);
            return FailureResponse("Failed", assignTask.Message);
        }

        [HttpPost("GetMyTask")]
        public async Task<Dictionary<string, object>> GetMyTask()
        {
            var task = await _taskService.GetMyTask(UserId);
            if (task.Message == "Success")
                return APIResponse("Success", null);
            return FailureResponse(task.Message, task.Data);
        }

        [HttpPost("AcceptTask")]
        public async Task<Dictionary<string, object>> AcceptTask(AcceptTaskRequest request)
        {
            var userAction = await _taskService.UserAction(request, UserId);
            if (userAction.Message == "Success")
                return APIResponse("Success", null);
            return FailureResponse(userAction.Message, userAction.Data);
        }

        [HttpPost("UpdateTaskStatus")]
        public async Task<Dictionary<string, object>> UpdateTaskStatus(TaskStatusRequest request)
        {
            var updateStatus = await _taskService.UpdateStatus(request, UserId);
            if (updateStatus.Message == "Success")
                return APIResponse("Success", null);
            return FailureResponse(updateStatus.Message, updateStatus.Data);

        }
    }
}
