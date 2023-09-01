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
        Task<ResponseModel> AddAsync(AddUserDto requestDto);
        Task<ResponseModel> GetAllUsers(int companyId, PageResult pageResult);
        Task<List<UserDTO>> GetAsync(int companyId);
        Task<ResponseModel> GetByIdAsync(GetUserByIdDto requestDto);
        Task<ResponseModel> UpdateAsync(UpdateUserDto updateUserDto);
        Task<ResponseModel> DeleteAsync(DeleteUserDto deleteUserDto);
        Task<ResponseModel> Login(LoginDto request);
        Task<ResponseModel> ForgetPassword(ForgetPasswordDto request);
        Task<ResponseModel> ResetPassword(PasswordResetDto request);
        Task<ResponseModel> ValidateOtp(OTPValidateDto request);
    }
}
