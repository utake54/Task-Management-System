using AutoMapper;
using TaskManagement.Model.Model.Task;
using TaskManagement.API.Request;
using TaskManagement.Service.Entities.Task;
using TaskManagement.Model.Model.Task.DTO;

namespace TaskManagement.API.Infrastructure.AutoMapper
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<AddTaskRequest, AddTaskDto>().ReverseMap();
            CreateMap<GetTaskByIdRequest, GetTaskByIdDto>().ReverseMap();
            CreateMap<UpdateTaskRequest, UpdateTaskDto>().ReverseMap();
            CreateMap<DeleteTaskRequest, DeleteTaskDto>().ReverseMap();
            CreateMap<TaskMaster, AddTaskDto>().ReverseMap();
            CreateMap<TaskMaster, TaskExportDTO>().ReverseMap();
            CreateMap<AcceptTaskRequest, AcceptTaskDto>().ReverseMap();
            CreateMap<AssignTaskRequest, AssignTaskDto>().ReverseMap();
            CreateMap<TaskStatusRequest, TaskStatusDto>().ReverseMap();
            CreateMap<TaskMaster, TaskMasterDto>().ReverseMap();


        }
    }
}
