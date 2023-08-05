using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Model.Model.User.Request;
using TaskManagement.Service.Profile;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost("MyProfile")]
        public async Task<Dictionary<string, object>> MyProfile()
        {
            var profile = await _profileService.GetProfile(UserId);
            if (profile.Message == "Success")
                return APIResponse("Success", profile.Data);
            return FailureResponse(profile.Message, profile.Failure);
        }

        [HttpPost("UpdateProfile")]
        public async Task<Dictionary<string, object>> UpdateProfile(UserRequest request)
        {
            var profile = await _profileService.UpdateProfile(request);
            if (profile.Message == "Success")
                return APIResponse("Success", null);
            return FailureResponse(profile.Message, profile.Failure);
        }

    }
}
