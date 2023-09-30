using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using TaskManagement.API.Request;
using TaskManagement.Model.Model.SearchModel;
using TaskManagement.Service.Entities.Task;
using TaskManagement.Service.TaskService;

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

            return APIResponse(addTask.Result, addTask.Message, "TM035");
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
            return APIResponse(task.Data, task.Message);
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
            return APIResponse(updateTask.Result, updateTask.Message, "TM036");
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
            return APIResponse(deleteTask.Result, deleteTask.Message, "TM037");
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
            return APIResponse(allTask.Data, allTask.Message);
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
            return APIResponse(task.Data, task.Message);
        }
    }
}
