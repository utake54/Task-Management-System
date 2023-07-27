using AutoMapper;
using TaskManagement.Model.Model.Task.Request;
using TaskManagement.Model.Model.Task;
using TaskManagement.Model.Model.User;
using TaskManagement.Model.Model.User.DTO;
using TaskManagement.Model.Model.User.Request;
using TaskManagement.Model.Model.Category.DTO;
using TaskManagement.Model.Model.Category;
using TaskManagement.Model.Model.Category.Request;

namespace TaskManagement.API.Infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserRequest, UserMaster>().ReverseMap();
            CreateMap<UserMaster, UserDTO>().ReverseMap();
            CreateMap<TaskRequest, TaskMaster>().ReverseMap();
            CreateMap<CategoryDTO, TaskCategoryMaster>().ReverseMap();
            CreateMap<CategoryRequest, TaskCategoryMaster>().ReverseMap();

        }
    }
}
