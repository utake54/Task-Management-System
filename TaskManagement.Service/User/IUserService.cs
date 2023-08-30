using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.OTP;
using TaskManagement.Model.Model.PagedResult;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.User;
using TaskManagement.Model.Model.User.DTO;
using TaskManagement.Model.Model.User.Request;
using TaskManagement.Service.Entities.Login;
using TaskManagement.Service.Entities.User;

namespace TaskManagement.Service.UserService
{
    public interface IUserService
    {
        Task<ResponseModel> AddUser(AddUserDto requestDto);
        Task<ResponseModel> GetAllUsers(int companyId, PageResult pageResult);
        Task<List<UserDTO>> GetAllUsers(int companyId);
        Task<ResponseModel> GetUser(GetUserDto requestDto);
        Task<ResponseModel> UpdateUser(UpdateUserDto updateUserDto);
        Task<ResponseModel> DeleteUser(DeleteUserDto deleteUserDto);
        Task<ResponseModel> Login(LoginDto request);
        Task<ResponseModel> ForgetPassword(ForgetPasswordDto request);
        Task<ResponseModel> ResetPassword(PasswordResetDto request);
        Task<ResponseModel> ValidateOtp(OTPValidateDto request);
    }
}
