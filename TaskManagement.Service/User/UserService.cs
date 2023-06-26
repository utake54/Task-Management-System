using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Database.Repository.UserRepository;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.User.Request;

namespace TaskManagement.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public Task<ResponseModel> AddUser(UserRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> DeleteUser(int userId)
        {
            var response = new ResponseModel();
            var user = await _userRepository.GetById(userId);
            if (user == null)
            {
                response.Message = "No user found";
                return response;
            }
            response.Ok(user);
            return response;
        }

        public async Task<ResponseModel> GetAllUsers()
        {
            var response = new ResponseModel();
            var allUsers = await _userRepository.GetAllAsync();
            if (allUsers.Count() <= 0)
            {
                response.Message = "No users found";
                return response;
            }
            response.Ok(allUsers);
            return response;
        }

        public async Task<ResponseModel> GetUser(int userId)
        {
            var response = new ResponseModel();
            var user = await _userRepository.GetById(userId);
            if (user == null)
            {
                response.Message = "No user found";
                return response;
            }
            response.Ok(user);
            return response;
        }

        public async Task<ResponseModel> UpdateUser(UserRequest request)
        {
            return null;
        }
    }
}
