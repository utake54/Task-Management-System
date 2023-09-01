﻿using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.User.Request;
using TaskManagement.Service.Entities.User;

namespace TaskManagement.Service.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseModel> GetAsync(int id)
        {
            var response = new ResponseModel();

            var profileData = await _unitOfWork.UserRepository.GetById(id);
            if (profileData == null)
            {
                response.Failure("User not found.");
                return response;
            }
            response.Ok(profileData);
            return response;
        }

        public async Task<ResponseModel> UpdateAsync(UpdateUserDto requestDto)
        {
            var response = new ResponseModel();

            var profileData = await _unitOfWork.UserRepository.GetById(requestDto.Id);
            if (profileData == null)
            {
                response.Failure("User not found.");
                return response;
            }
            _unitOfWork.UserRepository.Update(profileData);
            await _unitOfWork.SaveChangesAsync();
            response.Ok();
            return response;
        }
    }
}