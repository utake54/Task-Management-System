using AutoMapper;
using TaskManagement.Model.Model.Task.Request;
using TaskManagement.Model.Model.Task;
using TaskManagement.Model.Model.User;
using TaskManagement.Model.Model.User.DTO;
using TaskManagement.Model.Model.User.Request;

namespace TaskManagement.API.Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserRequest, UserMaster>();
            CreateMap<UserMaster, UserDTO>();
            CreateMap<TaskRequest, TaskMaster>();

        }
    }
}
