using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Service.Entities.User;

namespace TaskManagement.Service.Profile
{
    public interface IProfileService
    {
        Task<ResponseModel> GetAsync(int id);
        Task<ResponseModel> UpdateAsync(UpdateUserDto requestDto);
    }
}
