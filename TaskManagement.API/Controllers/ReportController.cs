using AutoMapper;
using GemBox.Document;
using GemBox.Pdf;
using GemBox.Pdf.Content;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NPOI.XSSF.UserModel;
using System.Net.Mail;
using TaskManagement.API.Infrastructure.Filters;
using TaskManagement.Service.Reports;
using TaskManagement.Service.TaskService;
using TaskManagement.Service.UserService;
using TaskManagement.Utility;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[Permissible("Admin", "HOD")]
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

        [HttpPost("TestUploadDocs")]
        public async Task<IActionResult> UploadAsync(List<IFormFile> formFiles)
        {
            GemBox.Pdf.ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            var tempDir = Path.Combine(Directory.GetCurrentDirectory(), "Temp");
            Directory.CreateDirectory(tempDir);
            var pdfFileNames = new List<string>();

            foreach (var file in formFiles)
            {
                if (file.Length > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(tempDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    pdfFileNames.Add(filePath);
                }
            }

            using (var document = new PdfDocument())
            {
                var customPage = document.Pages.Add();
                var formatedText = new PdfFormattedText();
                formatedText.AppendLine("Test 1St Page");
                double x = 100, y = customPage.CropBox.Top - 100 - formatedText.Height;
                customPage.Content.DrawText(formatedText,new PdfPoint(x,y));
                // Merge multiple PDF files into single PDF by loading source documents
                // and cloning all their pages to destination document.
                foreach (var fileName in pdfFileNames)
                    using (var source = PdfDocument.Load(fileName))
                        document.Pages.Kids.AddClone(source.Pages);

               
                document.Save("Merge Files.pdf");
            }

            return Ok();
        }


    }
}
