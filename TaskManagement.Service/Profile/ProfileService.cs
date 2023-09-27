using AutoMapper;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.User.DTO;
using TaskManagement.Service.Entities.User;

namespace TaskManagement.Service.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProfileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseModel> GetAsync(int id)
        {
            var response = new ResponseModel();

            var profileData = await _unitOfWork.UserRepository.GetById(id);
            if (profileData == null)
            {
                response.Failure("TM008");
                return response;
            }
            var profileDto = _mapper.Map<UserDTO>(profileData);
            response.Ok(profileDto);
            return response;
        }

        public async Task<ResponseModel> UpdateAsync(UpdateUserDto requestDto)
        {
            var response = new ResponseModel();

            var profileData = await _unitOfWork.UserRepository.GetById(requestDto.Id);
            if (profileData == null)
            {
                response.Failure("TM008");
                return response;
            }
            _unitOfWork.UserRepository.Update(profileData);
            await _unitOfWork.SaveChangesAsync();
            response.Ok();
            return response;
        }
    }
}
