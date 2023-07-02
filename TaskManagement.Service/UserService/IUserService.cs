using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.Login.Request;
using TaskManagement.Model.Model.OTP;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.User;
using TaskManagement.Model.Model.User.Request;

namespace TaskManagement.Service.UserService
{
    public interface IUserService
    {
        Task<ResponseModel> AddUser(UserRequest request, int userId, int companyId);
        Task<ResponseModel> GetAllUsers(int companyId);
        Task<ResponseModel> GetUser(int userId);
        Task<ResponseModel> UpdateUser(int userId, UserRequest request);
        Task<ResponseModel> DeleteUser(int userId);
        Task<UserMaster> Login(LoginRequest request);
        Task<ResponseModel> ForgetPassword(ForgetPassswordRequest request);
        Task<ResponseModel> ResetPassword(PasswordResetRequest request);
        Task<ResponseModel> ValidateOtp(OTPValidateRequest request);
    }
}
