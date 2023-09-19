﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NPOI.XSSF.UserModel;
using System.ComponentModel.Design;
using TaskManagement.Model.Model.Task.Request;
using TaskManagement.Service.TaskService;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using TaskManagement.Utility;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskActionController : BaseController
    {
        private readonly ITaskService _taskService;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for dependancy Ijection
        /// </summary>
        /// <param name="taskService"></param>
        public TaskActionController(ITaskService taskService, IDistributedCache distributedCache, IMapper mapper)
        {
            _taskService = taskService;
            _distributedCache = distributedCache;
            _mapper = mapper;
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

            return APIResponse(assignTask.Result, assignTask.Message, "TM045");
        }


        /// <summary>
        /// Get task assigned to me
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetMyTask")]
        public async Task<Dictionary<string, object>> GetMyTask()
        {
            var task = await _taskService.GetMyTask(UserId);
            return APIResponse(task.Data, task.Message);
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
            return APIResponse(userAction.Result, userAction.Message, "TM046");
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
            return APIResponse(updateStatus.Result, updateStatus.Message, "TM047");
        }

    }
}
