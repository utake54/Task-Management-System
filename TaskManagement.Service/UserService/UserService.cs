using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Database.Repository.UserRepository;
using TaskManagement.Model.Model.Login.Request;
using TaskManagement.Model.Model.OTP;
using TaskManagement.Model.Model.PagedResult;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.User;
using TaskManagement.Model.Model.User.DTO;
using TaskManagement.Model.Model.User.Request;
using TaskManagement.Service.OTPService;
using TaskManagement.Utility;
using TaskManagement.Utility.Email;

namespace TaskManagement.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISendMail _sendMail;
        private readonly IOTPService _otpService;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ISendMail sendMail, IOTPService oTPService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sendMail = sendMail;
            _otpService = oTPService;
        }

        public async Task<ResponseModel> AddUser(UserRequest request, int userId, int companyId)
        {
            var response = new ResponseModel();
            var user = _mapper.Map<UserRequest, UserMaster>(request);
            var systemPassword = request.LastName.ToString() + request.MobileNo.ToString();
            user.IsActive = true;
            user.CreatedBy = userId;
            user.CompanyId = companyId;
            user.CreatedDate = DateTime.Now;
            user.Password = SHA.Encrypt(systemPassword);
            user.DateOfBirth = Convert.ToDateTime(request.DateOfBirth);
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.UserRepository.SaveChanges();
            await _sendMail.SendEmailAsync("omkarutake@nimapinfotech.com", null, "Registration successfull", $"Your Password is {systemPassword}");
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

        public async Task<ResponseModel> GetAllUsers(int companyId, PageResult pageResult)
        {
            var response = new ResponseModel();
            var allUsers = await _unitOfWork.UserRepository.GetAllUsers(companyId, pageResult.PageNumber, pageResult.PageSize);
            if (!allUsers.Any())
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
            user.EmailId = request.EmailId;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.UserRepository.SaveChanges();

            response.Ok(user);
            return response;
        }
        public async Task<ResponseModel> Login(LoginRequest request)
        {
            var response = new ResponseModel();
            request.Password = SHA.Encrypt(request.Password);
            var user = await _unitOfWork.UserRepository.GetUserDetails(request);
            if (user != null)
            {
                response.Ok(user);
                return response;
            }
            response.Failure("Invalid Credentialsss");
            return response;
        }

        public async Task<ResponseModel> ForgetPassword(ForgetPassswordRequest request)
        {
            var response = new ResponseModel();
            var isUserExists = await _unitOfWork.UserRepository.GetDefault(x => x.EmailId == request.EmailOrMobile || x.MobileNo == request.EmailOrMobile && x.DateOfBirth == Convert.ToDateTime(request.DateOfBirth));
            if (isUserExists != null)
            {
                var userEmail = isUserExists.EmailId;
                var otp = OTPGenerator.GetOTP();
                await _sendMail.SendEmailAsync("utake.omkar54@gmail.com", "notes.dac@gmail.com", "OTP for password reset", otp.ToString());
                var saveOTP = _otpService.AddOTP(isUserExists.Id, otp);
                response.Ok(isUserExists.Id, "OTP sent successfully on email");

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

        public Task<ResponseModel> ValidateOtp(OTPValidateRequest request)
        {
            var response = new ResponseModel();

            return null;
        }

        public async Task<List<UserDTO>> GetAllUsers(int companyId)
        {


            var users = await _unitOfWork.UserRepository.Get(x => x.CompanyId == companyId);

            var usersDTO = new List<UserDTO>();
            foreach (var user in users)
            {
                var userDTO = _mapper.Map<UserMaster, UserDTO>(user);
                usersDTO.Add(userDTO);
            }
            return usersDTO;
        }
    }
}
