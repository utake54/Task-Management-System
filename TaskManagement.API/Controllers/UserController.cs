using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Service.UserService;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("GetAllUsers")]
        public async Task<IActionResult> GetAllUser()
        {
            var allUser = await _userService.GetAllUsers();
            return Ok(allUser);
        }
    }
}
