using AutoMapper;
using TaskManagement.Model.Model.Task.Request;
using TaskManagement.Model.Model.Task;
using TaskManagement.API.Request;
using TaskManagement.Service.Entities.Task;

namespace TaskManagement.API.Infrastructure.AutoMapper
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<TaskRequest, TaskMaster>().ReverseMap();
            CreateMap<AddTaskRequest, AddTaskDto>().ReverseMap();
            CreateMap<GetTaskByIdRequest, GetTaskByIdDto>().ReverseMap();
            CreateMap<UpdateTaskRequest, UpdateTaskDto>().ReverseMap();
            CreateMap<DeleteTaskRequest, DeleteTaskDto>().ReverseMap();
            CreateMap<TaskMaster, AddTaskDto>().ReverseMap();


        }
    }
}
