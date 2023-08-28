using AutoMapper;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.OTP;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Service.Entities.Login;

namespace TaskManagement.Service.OTPService
{
    public class OTPService : IOTPService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OTPService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> AddOTP(int userId, int otp)
        {
            var otpDetails = new OTPMaster();
            otpDetails.UserId = userId;
            otpDetails.OTP = otp;
            otpDetails.GeneratedTime = DateTime.UtcNow;
            otpDetails.ExpiryTime = DateTime.UtcNow.AddMinutes(10);

            var saveOTP = _unitOfWork.OTPRepository.AddAsync(otpDetails);
            await _unitOfWork.SaveChangesAsync();
            return userId;
        }

        public async Task<ResponseModel> ValidateOTP(OTPValidateDto request)
        {
            var response = new ResponseModel();
            var isValidOTP = await _unitOfWork.OTPRepository.IsValidOTP(request);
            if (isValidOTP != null)
            {
                isValidOTP.IsActive = false;
                _unitOfWork.OTPRepository.Update(isValidOTP);
                await _unitOfWork.SaveChangesAsync();
                response.Ok();
                return response;
            }
            response.Failure("Invalid OTP");
            return response;
        }
    }
}
