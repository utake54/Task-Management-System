using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.SearchModel;
using TaskManagement.Model.Model.Task;
using TaskManagement.Model.Model.Task.DTO;
using TaskManagement.Model.Model.Task.Request;
using TaskManagement.Utility.Email;
using TaskManagement.Utility.Enum;

namespace TaskManagement.Service.TaskService
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISendMail _sendMail;
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper, ISendMail sendMail)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sendMail = sendMail;
        }

        public async Task<ResponseModel> AddTask(int userId, TaskRequest request, int companyId)
        {
            var response = new ResponseModel();
            var taskMaster = _mapper.Map<TaskRequest, TaskMaster>(request);
            taskMaster.CreatedBy = userId;
            taskMaster.CreatedDate = DateTime.Now;
            taskMaster.CompanyId = companyId;
            await _unitOfWork.TaskRepository.AddAsync(taskMaster);
            await _unitOfWork.SaveChangesAsync();
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
                await _unitOfWork.SaveChangesAsync();
                response.Ok();
                return response;
            }
            response.Failure("No task found");
            return response;
        }

        public async Task<ResponseModel> GetAllTask(int companyId, SearchModel search)
        {
            var response = new ResponseModel();
            var taskList = await _unitOfWork.TaskRepository.GetAllTask(companyId, search);
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
                await _unitOfWork.SaveChangesAsync();
                response.Ok(task);
                return response;
            }
            response.Failure("No task found");
            return response;
        }
        public async Task<ResponseModel> AssignTask(AssignTaskRequest request, int userId, int companyId)
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
                    EndDate = DateTime.Now,

                };
                taskList.Add(task);
            }
            await _unitOfWork.AssignTaskRepository.AddRangeAsync(taskList);

            var users = await _unitOfWork.UserRepository.Get(x => x.CompanyId == companyId);
            var taskAssignedUers = users.Where(x => x.Id == request.Id).Select(x => x.EmailId).ToList();
            taskAssignedUers.Add("utake.omkar54@gmail.com");
            taskAssignedUers.Add("omkar.utake54@gmail.com");

            var emailDetails = new MailDetails()
            {
                To = taskAssignedUers,
                CC = taskAssignedUers,
                Subject = "New task assigned",
                Message = "Dear Team,  <br> <p>New task assigned to you </p>"
            };

            await _sendMail.SendEmailAsync(emailDetails);
            await _unitOfWork.SaveChangesAsync();
            response.Ok();
            return response;
        }

        public async Task<ResponseModel> UserAction(AcceptTaskRequest request, int userId)
        {
            var response = new ResponseModel();
            var task = await _unitOfWork.AssignTaskRepository.GetDefault(x => x.UserId == userId && x.TaskId == request.TaskId);
            if (task == null)
            {
                response.Failure("No task found");
                return response;
            }
            task.IsAcceptByUser = request.IsAccepted;
            _unitOfWork.AssignTaskRepository.Update(task);
            await _unitOfWork.SaveChangesAsync();
            response.Ok();
            return response;

        }

        public async Task<ResponseModel> UpdateStatus(TaskStatusRequest request, int userId)
        {
            var response = new ResponseModel();
            var task = await _unitOfWork.AssignTaskRepository.GetDefault(x => x.TaskId == request.TaskId && x.UserId == userId);
            if (task == null)
            {
                response.Failure("No task found");
            }
            task.Status = request.StatusId;
            if (request.StatusId == (int)Status.Completed)
            {
                task.EndDate = DateTime.Now;
            }
            _unitOfWork.AssignTaskRepository.Update(task);
            await _unitOfWork.SaveChangesAsync();
            response.Ok();
            return response;
        }
            
        public async Task<ResponseModel> GetMyTask(int userId)
        {
            var response = new ResponseModel();
            var taskList = await _unitOfWork.TaskRepository.GetMyTask(userId);
            if (taskList.Any())
            {
                response.Ok(taskList);
                return response;
            }
            response.Failure("No task found");
            return response;
        }

        public async Task<List<TaskExportDTO>> GetTaskData(int companyId)
        {
            var data = await _unitOfWork.TaskRepository.GetTaskDetails(companyId);
            return data;
        }
    }
}
