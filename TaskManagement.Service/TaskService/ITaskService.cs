using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.SearchModel;
using TaskManagement.Model.Model.Task.DTO;
using TaskManagement.Model.Model.Task.Request;

namespace TaskManagement.Service.TaskService
{
    public interface ITaskService
    {
        Task<ResponseModel> AddTask(int userId, TaskRequest request,int companyId);
        Task<ResponseModel> GetTask(int taskId);
        Task<ResponseModel> DeleteTask(int taskId);
        Task<ResponseModel> UpdateTask(TaskRequest request, int userId);
        Task<ResponseModel> GetAllTask(int companyId, SearchModel search);
        Task<ResponseModel> AssignTask(AssignTaskRequest request, int userId, int companyId);
        Task<ResponseModel> UserAction(AcceptTaskRequest request, int userId);
        Task<ResponseModel> UpdateStatus(TaskStatusRequest request, int userId);
        Task<ResponseModel> GetMyTask(int userId);
        Task<List<TaskExportDTO>> GetTaskData(int companyId);

    }
}
