using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.Task;
using TaskManagement.Model.Model.Task.Request;

namespace TaskManagement.Service.TaskService
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> AddTask(int userId, TaskRequest request)
        {
            var response = new ResponseModel();
            var taskMaster = _mapper.Map<TaskRequest, TaskMaster>(request);
            taskMaster.CreatedBy = userId;
            taskMaster.CreatedDate = DateTime.Now;
            await _unitOfWork.TaskRepository.AddAsync(taskMaster);
            await _unitOfWork.TaskRepository.SaveChanges();
            response.Ok();
            return response;
        }


        public async Task<ResponseModel> DeleteTask(int taskId)
        {
            var response = new ResponseModel();
            var task = await _unitOfWork.TaskRepository.GetById(taskId);
            if (task != null)
            {
                _unitOfWork.TaskRepository.Delete(task);
                await _unitOfWork.TaskRepository.SaveChanges();
                response.Ok();
                return response;
            }
            response.Failure("No task found");
            return response;
        }

        public async Task<ResponseModel> GetAllTask(int companyId)
        {
            var response = new ResponseModel();
            var taskList = await _unitOfWork.TaskRepository.Get(x => x.CompanyId == companyId);
            if (taskList.Any())
            {
                response.Ok(taskList);
                return response;
            }
            response.Failure("No task found");
            return response;
        }

        public async Task<ResponseModel> GetTask(int taskId)
        {
            var response = new ResponseModel();
            var task = await _unitOfWork.TaskRepository.GetById(taskId);
            if (task != null)
            {
                response.Ok(task);
                return response;
            }
            response.Failure("No task found");
            return response;
        }

        public async Task<ResponseModel> UpdateTask(TaskRequest request, int userId)
        {
            var response = new ResponseModel();
            var task = await _unitOfWork.TaskRepository.GetById(request.Id);
            if (task != null)
            {
                task.Priority = request.Priority;
                task.Description = request.Description;
                task.DueDate = request.DueDate;
                task.Title = request.Title;
                task.ModifiedBy = userId;
                task.ModifiedDate = DateTime.Now;

                _unitOfWork.TaskRepository.Update(task);
                await _unitOfWork.TaskRepository.SaveChanges();
                response.Ok(task);
                return response;
            }
            response.Failure("No task found");
            return response;
        }
        public async Task<ResponseModel> AssignTask(AssignTaskRequest request, int userId)
        {
            var response = new ResponseModel();

            var taskList = new List<AssignTask>();
            foreach (var user in request.UserId)
            {
                var task = new AssignTask()
                {
                    TaskId = request.TaskId,
                    UserId = user,
                    AssignedDate = DateTime.Now,
                    AssignedBy = userId,
                    EndDate = request.EndDate,
                    Status = 1
                };
                taskList.Add(task);
            }
            
            await _unitOfWork.AssignTaskRepository.AddRangeAsync(taskList);
            await _unitOfWork.AssignTaskRepository.SaveChanges();
            response.Ok(null, "Task assigned successfully");
            return response;
        }
    }
}
