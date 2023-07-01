using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Database.Repository.UserRepository;
using TaskManagement.Model.Model.Login.Request;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.User;
using TaskManagement.Model.Model.User.DTO;
using TaskManagement.Model.Model.User.Request;
using TaskManagement.Utility;

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
            user.Password = SHA.Encrypt(request.Password);
            user.DateOfBirth = Convert.ToDateTime(request.DateOfBirth);
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
            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.UserRepository.SaveChanges();
            response.Ok();
            return response;
        }

        public async Task<ResponseModel> GetAllUsers(int companyId)
        {
            var response = new ResponseModel();
            var allUsers = await _unitOfWork.UserRepository.Get(x => x.CompanyId == companyId);
            if (allUsers.Count() <= 0)
            {
                response.Message = "No users found";
                return response;
            }
            var users = new List<UserDTO>();
            foreach (var user in allUsers)
            {
                var userDTO = _mapper.Map<UserMaster, UserDTO>(user);
                userDTO.DateOfBirth = user.DateOfBirth.ToString("d");
                users.Add(userDTO);
            }
            response.Ok(users);
            return response;
        }

        public async Task<ResponseModel> GetUser(int userId)
        {
            var response = new ResponseModel();
            var user = await _unitOfWork.UserRepository.GetById(userId);
            var userDTO = _mapper.Map<UserMaster, UserDTO>(user);
            if (user == null)
            {
                response.Message = "No user found";
                return response;
            }
            response.Ok(userDTO);
            return response;
        }


        public async Task<ResponseModel> UpdateUser(int userId, UserRequest request)
        {
            var response = new ResponseModel();
            var user = await _unitOfWork.UserRepository.GetById(request.Id);
            if (user == null)
            {
                response.Message = "No user found";
                return response;
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Address = request.Address;
            user.City = request.City;
            user.State = request.State;
            user.ZipCode = request.ZipCode;
            user.DateOfBirth = Convert.ToDateTime(request.DateOfBirth);
            user.MobileNo = request.MobileNo;
            user.ModifiedBy = userId;
            user.ModifiedDate = DateTime.Now;

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.UserRepository.SaveChanges();

            response.Ok(user);
            return response;
        }
        public async Task<UserMaster> Login(LoginRequest request)
        {
            request.Password = SHA.Encrypt(request.Password);
            var user = await _unitOfWork.UserRepository.GetDefault(x => x.EmailId == request.UserId && x.Password == request.Password);
            return user;
        }

        public async Task<ResponseModel> ForgetPassword(ForgetPassswordRequest request)
        {
            var response = new ResponseModel();
            var isUserExists = await _unitOfWork.UserRepository.GetDefault(x => x.EmailId == request.EmailOrMobile || x.MobileNo == request.EmailOrMobile && x.DateOfBirth == Convert.ToDateTime(request.DateOfBirth));
            if (isUserExists != null)
            {
                response.Ok(isUserExists.Id);
                return response;
            }
            response.Failure("Invalid user details.");
            return response;
        }

        public async Task<ResponseModel> ResetPassword(PasswordResetRequest request)
        {
            var response = new ResponseModel();
            var user = await _unitOfWork.UserRepository.GetById(request.UserId);
            if (user == null)
            {
                response.Failure("No User Found");
                return response;
            }
            if (request.Password == request.ConfirmPassword)
            {
                user.Password = SHA.Encrypt(request.Password);
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.UserRepository.SaveChanges();

                response.Ok("Password changed successfully.");
                return response;
            }
            response.Failure("Please enter same password.");
            return response;
        }
    }
}
