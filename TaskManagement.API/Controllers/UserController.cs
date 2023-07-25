using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.XSSF.UserModel;
using TaskManagement.API.Infrastructure.Filters;
using TaskManagement.Model.Model.PagedResult;
using TaskManagement.Model.Model.User.DTO;
using TaskManagement.Model.Model.User.Request;
using TaskManagement.Service.UserService;
using TaskManagement.Utility;
using TaskManagement.Utility.Email;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Permissible("Admin","HOD")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ISendMail _sendMail;
        public UserController(IUserService userService, ISendMail sendMail)
        {
            _userService = userService;
            _sendMail = sendMail;
        }

        [HttpPost("GetAllUsers")]

        public async Task<Dictionary<string, object>> GetAllUser(PageResult pageResult)
        {
            var allUser = await _userService.GetAllUsers(CompanyId, pageResult);
            if (allUser.Message == "Success")
                return APIResponse("Success", allUser.Data);
            return FailureResponse("Failed", allUser.Message);
        }

        [HttpPost("AddUser")]
        public async Task<Dictionary<string, object>> AddUser([FromBody] UserRequest request)
        {
            var addUser = await _userService.AddUser(request, UserId, CompanyId);
            if (addUser.Message == "Success")
                return APIResponse("Success", addUser.Data);
            return FailureResponse("Failed", addUser.Message);
        }

        [HttpPost("GetUser/{userId}")]
        public async Task<Dictionary<string, object>> GetUser(int userId)
        {
            var user = await _userService.GetUser(userId);
            if (user.Message == "Success")
                return APIResponse("Success", user.Data);
            return FailureResponse("Failed", user.Message);
        }

        [HttpPost("DeleteUser/{userId}")]
        public async Task<Dictionary<string, object>> DeleteUser(int userId)
        {
            var deleteUser = await _userService.DeleteUser(userId);
            if (deleteUser.Message == "Success")
                return APIResponse("Success", deleteUser.Data);
            return FailureResponse("Failed", deleteUser.Message);
        }

        [HttpPost("UpdateUser")]
        public async Task<Dictionary<string, object>> UpdateUser([FromBody] UserRequest request)
        {
            var updateuser = await _userService.UpdateUser(UserId, request);
            if (updateuser.Message == "Success")
                return APIResponse("Success", updateuser.Data);
            return FailureResponse("Failed", updateuser.Message);
        }

        [HttpPost("ExportUsers")]
        public async Task<IActionResult> ExportUsers()
        {
            var allUsers = await _userService.GetAllUsers(UserId);
            if(allUsers.Count == 0)
            {
                return Ok("No users to export");
            }

            string fileName = $"UserImport-{DateTime.Now:MMddyyyyHHmmss}.xlsx";
            var workbook = new XSSFWorkbook();
            var sheetName = workbook.CreateSheet(fileName);
            ExportImportHelper.WriteData(allUsers, workbook, sheetName);        
            var memoryStream = new MemoryStream();
            workbook.Write(memoryStream);
            return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);


        }
    }
}
