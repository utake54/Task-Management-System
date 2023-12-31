﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Request;
using TaskManagement.Service.Entities.User;
using TaskManagement.Service.Profile;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

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

        [HttpPost("ViewAsync")]
        public async Task<Dictionary<string, object>> GetAsync()
        {
            var profile = await _profileService.GetAsync(UserId);
                return APIResponse(profile.Data, profile.Message);
        }

        [HttpPost("UpdateAsync")]
        public async Task<Dictionary<string, object>> UpdateAsync(UpdateUserRequest request)
        {
            var requestDto = _mapper.Map<UpdateUserDto>(request);
            var profile = await _profileService.UpdateAsync(requestDto);
            return APIResponse(profile.Result, profile.Message, "TM048");
        }

    }
}
