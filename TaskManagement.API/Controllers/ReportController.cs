using AutoMapper;
using GemBox.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NPOI.XSSF.UserModel;
using TaskManagement.API.Infrastructure.Filters;
using TaskManagement.Service.Reports;
using TaskManagement.Service.TaskService;
using TaskManagement.Service.UserService;
using TaskManagement.Utility;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Permissible("Admin", "HOD")]
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;
        private readonly IDistributedCache _distributedCache;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor for dependancy Ijection
        /// </summary>
        /// <param name="taskService"></param>
        public ReportController(IReportService reportService,
                                IDistributedCache distributedCache,
                                IMapper mapper,
                                IUserService userService)
        {
            _reportService = reportService;
            _distributedCache = distributedCache;
            _mapper = mapper;
            _userService = userService;
        }


        [HttpPost("ExportTaskDetails")]
        public async Task<IActionResult> ExportTaskDetails()
        {
            var result = await _reportService.TaskReport(CompanyId);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"TaskReport {DateTime.Now}");
        }


        [HttpPost("ExportUsersAsync")]
        public async Task<IActionResult> ExportAsync()
        {
            var result = await _reportService.UserReport(CompanyId);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"UserReport {DateTime.Now}");
        }
    }
}
