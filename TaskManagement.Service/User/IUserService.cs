using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.User.Request;

namespace TaskManagement.Service.UserService
{
    public interface IUserService
    {
        Task<ResponseModel> AddUser(UserRequest request);
        Task<ResponseModel> GetAllUsers();
        Task<ResponseModel> GetUser(int userId);
        Task<ResponseModel> UpdateUser(UserRequest request);
        Task<ResponseModel> DeleteUser(int userId);
    }
}
