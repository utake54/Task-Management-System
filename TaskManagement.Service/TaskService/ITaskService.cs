using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.Task.Request;

namespace TaskManagement.Service.TaskService
{
    public interface ITaskService
    {
        Task<ResponseModel> AddTask(int userId, TaskRequest request);
        Task<ResponseModel> GetTask(int taskId);
        Task<ResponseModel> DeleteTask(int taskId);
        Task<ResponseModel> UpdateTask(TaskRequest request, int userId);
        Task<ResponseModel> GetAllTask(int companyId);
        Task<ResponseModel> AssignTask(AssignTaskRequest request, int userId);
        Task<ResponseModel> UserAction(AcceptTaskRequest request, int userId);
        Task<ResponseModel> UpdateStatus(TaskStatusRequest request, int userId);
        Task<ResponseModel> GetMyTask(int userId);
    }
}
