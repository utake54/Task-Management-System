using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Database.Repository.UserRepository;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.User;
using TaskManagement.Model.Model.User.Request;

namespace TaskManagement.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> AddUser(UserRequest request, int userId, int companyId)
        {
            var response = new ResponseModel();
            var user = _mapper.Map<UserRequest, UserMaster>(request);
            user.IsActive = true;
            user.CreatedBy = userId;
            user.CompanyId = companyId;
            user.CreatedDate = DateTime.Now;
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.UserRepository.SaveChanges();
            response.Ok();
            return response;
        }

        public async Task<ResponseModel> DeleteUser(int userId)
        {
            var response = new ResponseModel();
            var user = await _unitOfWork.UserRepository.GetById(userId);
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
            var allUsers = await _unitOfWork.UserRepository.GetAllAsync();
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
            var user = await _unitOfWork.UserRepository.GetById(userId);
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
            var response = new ResponseModel();
            var user = await _unitOfWork.UserRepository.GetById(request.Id);
            if (user == null)
            {
                response.Message = "No user found";
                return response;
            }
            response.Ok(user);
            return response;

        }
    }
}
