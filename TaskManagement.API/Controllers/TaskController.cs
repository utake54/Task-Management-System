using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NPOI.XSSF.UserModel;
using TaskManagement.API.Request;
using TaskManagement.Model.Model.SearchModel;
using TaskManagement.Model.Model.Task.Request;
using TaskManagement.Service.Entities.Task;
using TaskManagement.Service.TaskService;
using TaskManagement.Utility;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TaskController : BaseController
    {
        private readonly ITaskService _taskService;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for dependancy Ijection
        /// </summary>
        /// <param name="taskService"></param>
        public TaskController(ITaskService taskService, IDistributedCache distributedCache, IMapper mapper)
        {
            _taskService = taskService;
            _distributedCache = distributedCache;
            _mapper = mapper;
        }

        /// <summary>
        /// Add New Task
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("AddTask")]
        public async Task<Dictionary<string, object>> AddAsync(AddTaskRequest request)
        {
            var requestDto = _mapper.Map<AddTaskDto>(request);
            requestDto.CreatedBy = UserId;
            requestDto.CompanyId = CompanyId;
            var addTask = await _taskService.AddAsync(requestDto);

            if (addTask.Message == "Success")
                return APIResponse("Success", addTask.Data);
            return FailureResponse("Failed", addTask.Message);
        }

        /// <summary>
        /// Get task by Id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPost("GetTask")]
        public async Task<Dictionary<string, object>> GetAsync(GetTaskByIdRequest request)
        {
            var requestDto = _mapper.Map<GetTaskByIdDto>(request);
            var task = await _taskService.GetByIdAsync(requestDto);
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
        public async Task<Dictionary<string, object>> UpdateAsync(UpdateTaskRequest request)
        {
            var requestDto = _mapper.Map<UpdateTaskDto>(request);
            var updateTask = await _taskService.UpdateAsync(requestDto);
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
        public async Task<Dictionary<string, object>> DeleteAsync(DeleteTaskRequest request)
        {
            var requestDto = _mapper.Map<DeleteTaskDto>(request);
            var deleteTask = await _taskService.DeleteAsync(requestDto);
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
        public async Task<Dictionary<string, object>> GetAsync(SearchModel search)
        {
            int companyId = 1;
            var allTask = await _taskService.GetAsync(companyId, search);
            if (allTask.Message != "Success")
                return FailureResponse("Failed", allTask.Message);

            return APIResponse("Success", allTask.Data);
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
