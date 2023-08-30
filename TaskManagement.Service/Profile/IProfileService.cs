using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.User.Request;
using TaskManagement.Service.Entities.ModelDto;

namespace TaskManagement.Service.Profile
{
    public interface IProfileService
    {
        Task<ResponseModel> GetProfile(int id);
        Task<ResponseModel> UpdateProfile(UpdateUserDto requestDto);
    }
}
