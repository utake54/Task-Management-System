using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.ResponseModel;
using TaskManagement.Model.Model.User.Request;

namespace TaskManagement.Service.Profile
{
    public interface IProfileService
    {
        Task<ResponseModel> GetProfile(int id);
        Task<ResponseModel> UpdateProfile(UserRequest request);
    }
}
