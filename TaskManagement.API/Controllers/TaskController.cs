using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NPOI.XSSF.UserModel;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.SearchModel;
using TaskManagement.Model.Model.Task.Request;
using TaskManagement.Service.TaskService;
using TaskManagement.Utility;

namespace TaskManagement.API.Controllers
{
    /// <summary>
    /// Task Module Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class TaskController : BaseController
    {
        private readonly ITaskService _taskService;
        private readonly IDistributedCache _distributedCache;

        /// <summary>
        /// Constructor for dependancy Ijection
        /// </summary>
        /// <param name="taskService"></param>
        public TaskController(ITaskService taskService, IDistributedCache distributedCache)
        {
            _taskService = taskService;
            _distributedCache = distributedCache;
        }

        /// <summary>
        /// Add New Task
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("AddTask")]
        public async Task<Dictionary<string, object>> AddTask(TaskRequest request)
        {
            var addTask = await _taskService.AddTask(UserId, request, CompanyId);
            if (addTask.Message == "Success")
                return APIResponse("Success", addTask.Data);
            return FailureResponse("Failed", addTask.Message);
        }

        /// <summary>
        /// Get task by Id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPost("GetTask/{taskId}")]
        public async Task<Dictionary<string, object>> GetTask(int taskId)
        {
            var task = await _taskService.GetTask(taskId);
            if (task.Message == "Success")
                return APIResponse("Success", task.Data);
            return FailureResponse("Failed", task.Message);
        }


        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("UpdateTask")]
        public async Task<Dictionary<string, object>> UpdateTask(TaskRequest request)
        {
            var updateTask = await _taskService.UpdateTask(request, UserId);
            if (updateTask.Message == "Success")
                return APIResponse("Success", updateTask.Data);
            return FailureResponse("Failed", updateTask.Message);
        }

        /// <summary>
        /// Delete task by id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPost("DeleteTask/{taskId}")]
        public async Task<Dictionary<string, object>> DeleteTask(int taskId)
        {
            var deleteTask = await _taskService.DeleteTask(taskId);
            if (deleteTask.Message == "Success")
                return APIResponse("Success", deleteTask.Data);
            return FailureResponse("Failed", deleteTask.Message);
        }


        /// <summary>
        /// Get all task
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("GetAllTask")]
        public async Task<Dictionary<string, object>> GetAllTask(SearchModel search)
        {
            {
                var allTask = await _taskService.GetAllTask(CompanyId, search);
                if (allTask.Message != "Success")
                    return FailureResponse("Failed", allTask.Message);

                return APIResponse("Success", allTask.Data);
            }

        }
        /// <summary>
        /// Assign task to team
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("AssignToTeam")]
        public async Task<Dictionary<string, object>> AssignTask(AssignTaskRequest request)
        {
            var assignTask = await _taskService.AssignTask(request, UserId, CompanyId);

            if (assignTask.Message == "Success")
                return APIResponse("Success", assignTask.Data);
            return FailureResponse("Failed", assignTask.Message);
        }


        /// <summary>
        /// Get task assigned to me
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetMyTask")]
        public async Task<Dictionary<string, object>> GetMyTask()
        {
            var task = await _taskService.GetMyTask(UserId);
            if (task.Message == "Success")
                return APIResponse("Success", task.Data);
            return FailureResponse("Failed", task.Message);
        }


        /// <summary>
        /// Accept or reject task
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("AcceptTask")]
        public async Task<Dictionary<string, object>> AcceptTask(AcceptTaskRequest request)
        {
            var userAction = await _taskService.UserAction(request, UserId);
            if (userAction.Message == "Success")
                return APIResponse("Success", null);
            return FailureResponse(userAction.Message, userAction.Data);
        }


        /// <summary>
        /// Update task status 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("UpdateTaskStatus")]
        public async Task<Dictionary<string, object>> UpdateTaskStatus(TaskStatusRequest request)
        {
            var updateStatus = await _taskService.UpdateStatus(request, UserId);
            if (updateStatus.Message == "Success")
                return APIResponse("Success", null);
            return FailureResponse(updateStatus.Message, updateStatus.Data);
        }


        /// <summary>
        /// Export task and related details in excels sheet
        /// </summary>
        /// <returns></returns>
        [HttpPost("ExportTaskDetails")]
        public async Task<IActionResult> ExportTaskDetails()
        {
            var taskData = await _taskService.GetTaskData(CompanyId);

            if (taskData.Count == 0)
            {
                return Ok("No data found to export the file");
            }

            string fileName = $"TaskImport-{DateTime.Now:MMddyyyyHHmmss}.xlsx";
            var workbook = new XSSFWorkbook();
            var sheetName = workbook.CreateSheet(fileName);
            ExportImportHelper.WriteData(taskData, workbook, sheetName);
            var memoryStream = new MemoryStream();
            workbook.Write(memoryStream);
            return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }



        /// <summary>
        /// Get task by category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpPost("GetTaskByCategory/{categoryId}")]
        public async Task<Dictionary<string, object>> GetTaskByCategory(int categoryId)
        {
            var task = await _taskService.GetByCategories(categoryId);
            if (task.Message == "Success")
                return APIResponse("Success", task.Data);
            return FailureResponse(task.Message, null);
        }
    }
}
