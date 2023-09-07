﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Request;
using TaskManagement.Model.Model.PagedResult;
using TaskManagement.Service.Entities.User;
using TaskManagement.Service.UserService;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[Permissible("Admin", "HOD")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("GetAllAsync")]
        public async Task<Dictionary<string, object>> GetAllAsync([FromBody] PageResult pageResult)
        {
            var allUser = await _userService.GetAllUsers(1, pageResult);
            return NewAPIResponse(allUser.Data, allUser.Message);
        }

        //[Authorize("Admin")]
        [HttpPost("AddAsync")]
        public async Task<Dictionary<string, object>> AddAsync([FromBody] AddUserRequest request)
        {
            var requestDto = _mapper.Map<AddUserDto>(request);
            requestDto.CreatedBy = 1;
            requestDto.CompanyId = 1;
            var addUser = await _userService.AddAsync(requestDto);
            return NewAPIResponse(addUser.Result, addUser.Message, "User addedd successfully.");
        }

        [HttpPost("GetAsync")]
        public async Task<Dictionary<string, object>> GetAsync([FromBody] GetUserRequest request)
        {
            var requestDto = _mapper.Map<GetUserByIdDto>(request);
            var user = await _userService.GetByIdAsync(requestDto);
            return NewAPIResponse(user.Data, user.Message);
        }

        //[Authorize("Admin")]
        [HttpPost("DeleteAsync")]
        public async Task<Dictionary<string, object>> DeleteAsync([FromBody] DeleteUserRequest request)
        {
            var requestDto = _mapper.Map<DeleteUserDto>(request);
            requestDto.ActionBy = UserId;
            var deleteUser = await _userService.DeleteAsync(requestDto);
            return NewAPIResponse(deleteUser.Result, deleteUser.Message, "User deleted successfully.");
        }

        //[Authorize("Admin")]
        [HttpPost("UpdateAsync")]
        public async Task<Dictionary<string, object>> UpdateAsync([FromBody] UpdateUserRequest request)
        {
            var requestDto = _mapper.Map<UpdateUserDto>(request);
            requestDto.ModifiedBy = UserId;
            var updateuser = await _userService.UpdateAsync(requestDto);
            return NewAPIResponse(updateuser.Result, updateuser.Message, "User deleted successfully.");
        }
    }
}
