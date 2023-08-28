using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.User.Request;
using TaskManagement.Service.Entities.ModelDto;

namespace TaskManagement.Service.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseModel> GetProfile(int id)
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

        public async Task<ResponseModel> UpdateProfile(UserRequest request)
        {
            var response = new ResponseModel();

            var profileData = await _unitOfWork.UserRepository.GetById(request.Id);
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
