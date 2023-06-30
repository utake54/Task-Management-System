using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Model.Model.User.Request;
using TaskManagement.Service.UserService;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("GetAllUsers")]
        public async Task<Dictionary<string, object>> GetAllUser()
        {
            var allUser = await _userService.GetAllUsers();
            if (allUser.Message == "Success")
                return APIResponse("Success", allUser.Data);
            return FailureResponse("Failed", allUser.Message);
        }

        [HttpPost("AddUser")]
        public async Task<Dictionary<string, object>> AddUser(UserRequest request)
        {
            var addUser = await _userService.AddUser(request, UserId, CompanyId);
            if (addUser.Message == "Success")
                return APIResponse("Success", addUser.Data);
            return FailureResponse("Failed", addUser.Message);

        }
    }
}
