using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Infrastructure.Filters;
using TaskManagement.API.Request;
using TaskManagement.Model.Model.PagedResult;
using TaskManagement.Service.Entities.User;
using TaskManagement.Service.UserService;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Permissible("Admin", "HOD")]
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
            var allUser = await _userService.GetAllUsers(CompanyId, pageResult);
            return APIResponse(allUser.Data, allUser.Message);
        }

        [HttpPost("AddAsync")]
        public async Task<Dictionary<string, object>> AddAsync([FromBody] AddUserRequest request)
        {
            var requestDto = _mapper.Map<AddUserDto>(request);
            requestDto.CreatedBy = 1;
            requestDto.CompanyId = 1;
            var addUser = await _userService.AddAsync(requestDto);
            return APIResponse(addUser.Result, addUser.Message, "TM020");
        }

        [HttpPost("GetAsync")]
        public async Task<Dictionary<string, object>> GetAsync([FromBody] GetUserRequest request)
        {
            var requestDto = _mapper.Map<GetUserByIdDto>(request);
            var user = await _userService.GetByIdAsync(requestDto);
            return APIResponse(user.Data, user.Message);
        }

        [HttpPost("DeleteAsync")]
        public async Task<Dictionary<string, object>> DeleteAsync([FromBody] DeleteUserRequest request)
        {
            var requestDto = _mapper.Map<DeleteUserDto>(request);
            requestDto.ActionBy = UserId;
            var deleteUser = await _userService.DeleteAsync(requestDto);
            return APIResponse(deleteUser.Result, deleteUser.Message, "TM022");
        }

        [HttpPost("UpdateAsync")]
        public async Task<Dictionary<string, object>> UpdateAsync([FromBody] UpdateUserRequest request)
        {
            var requestDto = _mapper.Map<UpdateUserDto>(request);
            requestDto.ModifiedBy = UserId;
            var updateuser = await _userService.UpdateAsync(requestDto);
            return APIResponse(updateuser.Result, updateuser.Message, "TM021");
        }
    }
}
