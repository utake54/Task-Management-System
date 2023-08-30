using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.SearchModel;
using TaskManagement.Model.Model.Task.DTO;
using TaskManagement.Model.Model.Task.Request;
using TaskManagement.Service.Entities.Task;

namespace TaskManagement.Service.TaskService
{
    public interface ITaskService
    {
        Task<ResponseModel> AddTask(AddTaskDto addTaskDto);
        Task<ResponseModel> GetTask(GetTaskDto requestDto);
        Task<ResponseModel> DeleteTask(DeleteTaskDto requestDto);
        Task<ResponseModel> UpdateTask(UpdateTaskDto requestDto);
        Task<ResponseModel> GetAllTask(int companyId, SearchModel search);
        Task<ResponseModel> AssignTask(AssignTaskRequest request, int userId, int companyId);
        Task<ResponseModel> UserAction(AcceptTaskRequest request, int userId);
        Task<ResponseModel> UpdateStatus(TaskStatusRequest request, int userId);
        Task<ResponseModel> GetMyTask(int userId);
        Task<List<TaskExportDTO>> GetTaskData(int companyId);
        Task<ResponseModel> GetByCategories(int categoryId);
    }
}
