using AutoMapper;
using Azure;
using GemBox.Document;
using GemBox.Document.Tables;
using GemBox.Pdf;
using GemBox.Pdf.Content;
using Net.Pkcs11Interop.LowLevelAPI40.MechanismParams;
using NPOI.POIFS.FileSystem;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.User.DTO;
using TaskManagement.Utility;
using ComponentInfo = GemBox.Document.ComponentInfo;

namespace TaskManagement.Service.Reports
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            var userDto = _mapper.Map<IEnumerable<UserDTO>>(data);
            string fileName = $"UserImport-{DateTime.Now:MMddyyyyHHmmss}.xlsx";
            var workbook = new XSSFWorkbook();
            var sheetName = workbook.CreateSheet(fileName);
            ExportImportHelper.WriteData(userDto.ToList(), workbook, sheetName);
            var memoryStream = new MemoryStream();
            workbook.Write(memoryStream);
            return memoryStream.ToArray();
        }
    }

}
