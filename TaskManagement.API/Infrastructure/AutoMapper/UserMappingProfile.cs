using TaskManagement.Model.Model.User.DTO;
using TaskManagement.Model.Model.User;
using AutoMapper;
using TaskManagement.API.Request;
using TaskManagement.Service.Entities.User;
using TaskManagement.Service.Entities.Login;

namespace TaskManagement.API.Infrastructure.AutoMapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserMaster, UserDTO>().ReverseMap();
            CreateMap<AddUserRequest, AddUserDto>().ReverseMap();
            CreateMap<GetUserRequest, GetUserByIdDto>().ReverseMap();
            CreateMap<DeleteUserRequest, DeleteUserDto>().ReverseMap();
            CreateMap<UpdateUserRequest, UpdateUserDto>().ReverseMap();
            CreateMap<LoginRequest, LoginDto>().ReverseMap();
            CreateMap<ForgetPassswordRequest, ForgetPasswordDto>().ReverseMap();
            CreateMap<OTPValidateRequest, OTPValidateDto>().ReverseMap();
            CreateMap<PasswordResetRequest, PasswordResetDto>().ReverseMap();
        }
    }
}
