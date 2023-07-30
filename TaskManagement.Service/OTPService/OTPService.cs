using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.OTP;
using TaskManagement.Model.Model.ResponseModel;

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
            otpDetails.GeneratedTime = DateTime.Now;
            otpDetails.ExpiryTime = DateTime.Now.AddMinutes(10);

            var saveOTP = _unitOfWork.OTPRepository.AddAsync(otpDetails);
            await _unitOfWork.SaveChangesAsync();
            return userId;
        }

        public async Task<ResponseModel> ValidateOTP(OTPValidateRequest request)
        {
            var response = new ResponseModel();
            var isValidOTP = await _unitOfWork.OTPRepository.IsValidOTP(request);
            if (isValidOTP)
            {
                response.Ok();
                return response;
            }
            response.Failure("Invalid OTP");
            return response;
        }
    }
}
