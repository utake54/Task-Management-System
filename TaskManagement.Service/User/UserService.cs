using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Database.Repository.UserRepository;
using TaskManagement.Model.Model.OTP;
using TaskManagement.Model.Model.PagedResult;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.User;
using TaskManagement.Model.Model.User.DTO;
using TaskManagement.Model.Model.User.Request;
using TaskManagement.Service.Entities.Login;
using TaskManagement.Service.Entities.ModelDto;
using TaskManagement.Service.OTPService;
using TaskManagement.Utility;
using TaskManagement.Utility.Email;
using TaskManagement.Utility.Enum;

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

        public async Task<ResponseModel> AddUser(AddUserDto requestDto)
        {
            var response = new ResponseModel();

            #region User Validation for existing check
            var existingUsers = await _unitOfWork.UserRepository.GetAllAsync();


            var userExistWithEmail = existingUsers.Where(x => x.EmailId == requestDto.EmailId).Any();
            if (userExistWithEmail)
            {
                response.Failure("User already exists with same email id.");
                return response;
            }

            var userExistWithMobile = existingUsers.Where(x => x.MobileNo == requestDto.MobileNo).Any();
            if (userExistWithMobile)
            {
                response.Failure("User already exists with same Mobile no.");
                return response;
            }
            #endregion


            var data = new UserMaster
            {
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                EmailId = requestDto.EmailId,
                MobileNo = requestDto.MobileNo,
                DateOfBirth = requestDto.DateOfBirth,
                Address = requestDto.Address,
                City = requestDto.City,
                State = requestDto.State,
                Country = requestDto.Country,
                CountryCode = requestDto.CountryCode,
                ZipCode = requestDto.ZipCode,
                IsActive = true,
                CreatedBy = requestDto.CreatedBy,
                CompanyId = requestDto.CompanyId,
                CreatedDate = DateTime.UtcNow,
                Password = SHA.Encrypt(requestDto.FirstName + requestDto.LastName),
            };

            await _unitOfWork.UserRepository.AddAsync(data);
            await _unitOfWork.SaveChangesAsync();

            #region Mail Notification
            int templateId = (int)MailTemplate.Welcome;
            var getMailTemplate = await _unitOfWork.EmailTemplateRepository.GetById(templateId);
            var subject = getMailTemplate.Subject;
            var body = getMailTemplate.Body;
            body = body.Replace("@UserName", data.FirstName);
            body = body.Replace("@SysPass", data.Password);
            await _sendMail.SendEmailAsync(data.EmailId, null, subject, body);

            #endregion

            response.Ok();
            return response;
        }
        public async Task<ResponseModel> DeleteUser(DeleteUserDto deleteUserDto)
        {
            var response = new ResponseModel();
            var user = await _unitOfWork.UserRepository.GetById(deleteUserDto.Id);
            if (user == null)
            {
                response.Message = "No user found";
                return response;
            }
            user.ModifiedBy = deleteUserDto.ActionBy;
            user.ModifiedDate = DateTime.UtcNow;
            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.SaveChangesAsync();
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
        public async Task<ResponseModel> GetUser(GetUserDto requestDto)
        {
            var response = new ResponseModel();
            var user = await _unitOfWork.UserRepository.GetById(requestDto.Id);
            var userDTO = _mapper.Map<UserMaster, UserDTO>(user);
            if (user == null)
            {
                response.Message = "No user found";
                return response;
            }
            response.Ok(userDTO);
            return response;
        }
        public async Task<ResponseModel> UpdateUser(UpdateUserDto updateUserDto)
        {
            var response = new ResponseModel();
            var user = await _unitOfWork.UserRepository.GetById(updateUserDto.Id);
            if (user == null)
            {
                response.Message = "No user found";
                return response;
            }

            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Address = updateUserDto.Address;
            user.City = updateUserDto.City;
            user.State = updateUserDto.State;
            user.ZipCode = updateUserDto.ZipCode;
            user.DateOfBirth = Convert.ToDateTime(updateUserDto.DateOfBirth);
            user.MobileNo = updateUserDto.MobileNo;
            user.ModifiedBy = updateUserDto.ModifiedBy;
            user.ModifiedDate = DateTime.UtcNow;
            user.EmailId = updateUserDto.EmailId;
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            response.Ok(user);
            return response;
        }
        public async Task<ResponseModel> Login(LoginDto request)
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
        public async Task<ResponseModel> ForgetPassword(ForgetPasswordDto request)
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
        public async Task<ResponseModel> ResetPassword(PasswordResetDto request)
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
                await _unitOfWork.SaveChangesAsync();

                response.Ok("Password changed successfully.");
                return response;
            }
            response.Failure("Please enter same password.");
            return response;
        }
        public Task<ResponseModel> ValidateOtp(OTPValidateDto request)
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
