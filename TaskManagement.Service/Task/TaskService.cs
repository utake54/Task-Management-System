using AutoMapper;
using NPOI.SS.Formula.Functions;
using NPOI.XWPF.UserModel;
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
using TaskManagement.Service.Entities.Task;
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

        public async Task<ResponseModel> AddAsync(AddTaskDto requestDto)
        {
            var response = new ResponseModel();

            var taskMaster = _mapper.Map<TaskMaster>(requestDto);
            taskMaster.CreatedBy = requestDto.CreatedBy;
            taskMaster.CreatedDate = DateTime.UtcNow;
            taskMaster.CompanyId = requestDto.CompanyId;
            taskMaster.IsActive = true;
            await _unitOfWork.TaskRepository.AddAsync(taskMaster);
            await _unitOfWork.SaveChangesAsync();
            response.Ok();
            return response;
        }

        public async Task<ResponseModel> DeleteAsync(DeleteTaskDto requestDto)
        {
            var response = new ResponseModel();
            var task = await _unitOfWork.TaskRepository.GetById(requestDto.Id);
            if (task != null)
            {
                _unitOfWork.TaskRepository.Delete(task);
                await _unitOfWork.SaveChangesAsync();
                response.Ok();
                return response;
            }
            response.Failure("TM038");
            return response;
        }

        public async Task<ResponseModel> GetAsync(int companyId, SearchModel search)
        {
            var response = new ResponseModel();
            var taskList = await _unitOfWork.TaskRepository.GetAllTask(companyId, search);
            if (taskList.Tasks.Any())
            {
                response.Ok(taskList);
                return response;
            }
            response.Failure("TM038");
            return response;
        }

        public async Task<ResponseModel> GetByIdAsync(GetTaskByIdDto requestDto)
        {
            var response = new ResponseModel();
            var task = await _unitOfWork.TaskRepository.GetById(requestDto.Id);
            if (task != null)
            {
                response.Ok(task);
                return response;
            }

            response.Failure("TM038");
            return response;
        }

        public async Task<ResponseModel> UpdateAsync(UpdateTaskDto requestDto)
        {
            var response = new ResponseModel();
            var task = await _unitOfWork.TaskRepository.GetById(requestDto.Id);
            if (task != null)
            {
                task.Priority = requestDto.Priority;
                task.Description = requestDto.Description;
                task.DueDate = requestDto.DueDate;
                task.Title = requestDto.Title;
                task.ModifiedBy = requestDto.ActionBy;
                task.CategoryId = requestDto.CategoryId;
                task.ModifiedDate = DateTime.UtcNow;

                _unitOfWork.TaskRepository.Update(task);
                await _unitOfWork.SaveChangesAsync();
                response.Ok(task);
                return response;
            }
            response.Failure("TM038");
            return response;
        }
        public async Task<ResponseModel> AssignTask(AssignTaskDto request, int userId, int companyId)
        {
            var response = new ResponseModel();

            var taskList = new List<AssignTask>();
            foreach (var user in request.UserId)
            {
                var task = new AssignTask()
                {
                    TaskId = request.TaskId,
                    UserId = user,
                    AssignedDate = DateTime.UtcNow,
                    AssignedBy = userId,
                    EndDate = DateTime.UtcNow,
                    IsAcceptByUser = false,
                    Status=(int)Status.Assigned
                };
                taskList.Add(task);
            }
            await _unitOfWork.AssignTaskRepository.AddRangeAsync(taskList);
            await _unitOfWork.SaveChangesAsync();

            var users = await _unitOfWork.UserRepository.Get(x => x.CompanyId == companyId);
            var taskAssignedUers = new List<string>();
            foreach (var user in request.UserId)
            {
                taskAssignedUers = users.Where(x => x.Id == user).Select(x => x.EmailId).ToList();
            }
            var assignedBy = users.Where(x => x.Id == userId).ToList();
            var mailTemplate = await _unitOfWork.EmailTemplateRepository.GetById((int)MailTemplate.TaskAssigned);
            var taskDetails = await _unitOfWork.TaskRepository.GetById(request.TaskId);
            var mailBody = mailTemplate.Body;
            mailBody = mailBody.Replace("@UserName", "User");
            mailBody = mailBody.Replace("@AssignedDate", taskDetails.CreatedDate.ToShortDateString());
            mailBody = mailBody.Replace("@Title", taskDetails.Title);
            mailBody = mailBody.Replace("@Description", taskDetails.Description);
            mailBody = mailBody.Replace("@AssignedBy", assignedBy.Select(x => x.FirstName).First());
            mailBody = mailBody.Replace("@DueDate", taskDetails.DueDate.ToShortDateString());

            var emailDetails = new MailDetails()
            {
                To = taskAssignedUers,
                CC = assignedBy.Select(x => x.EmailId).ToList(),
                Subject = mailTemplate.Subject,
                Message = mailBody
            };

            await _sendMail.SendEmailAsync(emailDetails);
            response.Ok();
            return response;
        }

        public async Task<ResponseModel> UserAction(AcceptTaskDto request, int userId)
        {
            var response = new ResponseModel();
            var task = await _unitOfWork.AssignTaskRepository.GetDefault(x => x.UserId == userId && x.TaskId == request.TaskId);
            if (task == null)
            {
                response.Failure("TM038");
                return response;
            }
            task.IsAcceptByUser = request.IsAccepted;
            _unitOfWork.AssignTaskRepository.Update(task);
            await _unitOfWork.SaveChangesAsync();
            response.Ok();
            return response;
        }

        public async Task<ResponseModel> UpdateStatus(TaskStatusDto request, int userId)
        {
            var response = new ResponseModel();
            var task = await _unitOfWork.AssignTaskRepository.GetDefault(x => x.TaskId == request.TaskId && x.UserId == userId);
            if (task == null)
            {
                response.Failure("TM038");
            }
            task.Status = request.StatusId;
            if (request.StatusId == (int)Status.Completed)
            {
                task.EndDate = DateTime.UtcNow;
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
            response.Failure("TM038");
            return response;
        }

        public async Task<List<TaskExportDTO>> GetTaskData(int companyId)
        {
            var data = await _unitOfWork.TaskRepository.GetTaskDetails(companyId);
            return data;
        }

        public async Task<ResponseModel> GetByCategories(int categoryId)
        {
            var response = new ResponseModel();
            var taskList = await _unitOfWork.TaskRepository.Get(x => x.CategoryId == categoryId && x.IsActive == true);
            if (taskList.Any())
            {
                response.Ok(taskList);
                return response;
            }
            response.Failure("TM038");
            return response;
        }
    }
}
