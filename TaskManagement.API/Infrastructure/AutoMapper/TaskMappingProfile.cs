using AutoMapper;
using TaskManagement.Model.Model.Task.Request;
using TaskManagement.Model.Model.Task;

namespace TaskManagement.API.Infrastructure.AutoMapper
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<TaskRequest, TaskMaster>().ReverseMap();
        }
    }
}
