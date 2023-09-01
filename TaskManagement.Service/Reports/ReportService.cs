using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Utility;

namespace TaskManagement.Service.Reports
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<byte[]> TaskReport(int companyId)
        {
            var data = await _unitOfWork.TaskRepository.GetTaskDetails(companyId);
            string fileName = $"TaskImport-{DateTime.Now:MMddyyyyHHmmss}.xlsx";
            var workbook = new XSSFWorkbook();
            var sheetName = workbook.CreateSheet(fileName);
            ExportImportHelper.WriteData(data, workbook, sheetName);
            var memoryStream = new MemoryStream();
            workbook.Write(memoryStream);
            return memoryStream.ToArray();

        }

        public async Task<byte[]> UserReport(int companyId)
        {
            var data = await _unitOfWork.UserRepository.Get(x => x.CompanyId == companyId);


            string fileName = $"UserImport-{DateTime.Now:MMddyyyyHHmmss}.xlsx";
            var workbook = new XSSFWorkbook();
            var sheetName = workbook.CreateSheet(fileName);
            ExportImportHelper.WriteData(data.ToList(), workbook, sheetName);
            var memoryStream = new MemoryStream();
            workbook.Write(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
