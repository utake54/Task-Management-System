using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Request;
using TaskManagement.Service.Entities.User;
using TaskManagement.Service.Profile;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;
        public ProfileController(IProfileService profileService, IMapper mapper)
        {
            _profileService = profileService;
            _mapper = mapper;
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
        public async Task<Dictionary<string, object>> UpdateProfile(UpdateUserRequest request)
        {
            var requestDto = _mapper.Map<UpdateUserDto>(request);
            var profile = await _profileService.UpdateProfile(requestDto);
            if (profile.Message == "Success")
                return APIResponse("Success", null);
            return FailureResponse(profile.Message, profile.Failure);
        }

    }
}
