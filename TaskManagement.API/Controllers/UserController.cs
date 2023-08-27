using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.XSSF.UserModel;
using TaskManagement.API.Infrastructure.Filters;
using TaskManagement.API.Request;
using TaskManagement.Model.Model.PagedResult;
using TaskManagement.Model.Model.User.Request;
using TaskManagement.Service.Entities.ModelDto;
using TaskManagement.Service.UserService;
using TaskManagement.Utility;

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
            if (allUser.Message == "Success")
                return APIResponse("Success", allUser.Data);
            return FailureResponse("Failed", allUser.Message);
        }

        //[Authorize("Admin")]
        [HttpPost("AddAsync")]
        public async Task<Dictionary<string, object>> AddAsync([FromBody] AddUserRequest request)
        {
            var requestDto = _mapper.Map<AddUserDto>(request);
            requestDto.CreatedBy = 1;
            requestDto.CompanyId = 1;
            var addUser = await _userService.AddUser(requestDto);

            if (addUser.Message == "Success")
                return APIResponse("Success", addUser.Data);
            return FailureResponse("Failed", addUser.Message);
        }

        [HttpPost("GetAsync")]
        public async Task<Dictionary<string, object>> GetAsync([FromBody] GetUserRequest request)
        {
            var requestDto = _mapper.Map<GetUserDto>(request);
            var user = await _userService.GetUser(requestDto);
            if (user.Message == "Success")
                return APIResponse("Success", user.Data);
            return FailureResponse("Failed", user.Message);
        }

        [Authorize("Admin")]
        [HttpPost("DeleteAsync")]
        public async Task<Dictionary<string, object>> DeleteAsync([FromBody] DeleteUserRequest request)
        {
            var requestDto = _mapper.Map<DeleteUserDto>(request);
            requestDto.ActionBy = UserId;
            var deleteUser = await _userService.DeleteUser(requestDto);
            if (deleteUser.Message == "Success")
                return APIResponse("Success", deleteUser.Data);
            return FailureResponse("Failed", deleteUser.Message);
        }

        [Authorize("Admin")]
        [HttpPost("UpdateAsync")]
        public async Task<Dictionary<string, object>> UpdateAsync([FromBody] UpdateUserRequest request)
        {
            var requestDto = _mapper.Map<UpdateUserDto>(request);
            requestDto.ModifiedBy = UserId;
            var updateuser = await _userService.UpdateUser(requestDto);
            if (updateuser.Message == "Success")
                return APIResponse("Success", updateuser.Data);
            return FailureResponse("Failed", updateuser.Message);
        }

        [HttpPost("ExportUsersAsync")]
        public async Task<IActionResult> ExportAsync()
        {
            var allUsers = await _userService.GetAllUsers(UserId);
            if (allUsers.Count == 0)
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
