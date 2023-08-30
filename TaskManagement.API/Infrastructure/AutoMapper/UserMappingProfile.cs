using TaskManagement.Model.Model.User.DTO;
using TaskManagement.Model.Model.User.Request;
using TaskManagement.Model.Model.User;
using AutoMapper;
using TaskManagement.API.Request;
using TaskManagement.Service.Entities.User;

namespace TaskManagement.API.Infrastructure.AutoMapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRequest, UserMaster>().ReverseMap();
            CreateMap<UserMaster, UserDTO>().ReverseMap();
            CreateMap<AddUserRequest, AddUserDto>().ReverseMap();
            CreateMap<GetUserRequest, GetUserDto>().ReverseMap();
            CreateMap<DeleteUserRequest, DeleteUserDto>().ReverseMap();
            CreateMap<UpdateUserRequest, UpdateUserDto>().ReverseMap();
        }
    }
}
