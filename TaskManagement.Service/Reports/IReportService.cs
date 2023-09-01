using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Service.Reports
{
    public interface IReportService
    {
        Task<byte[]> TaskReport(int companyId);
        Task<byte[]> UserReport(int companyId);
    }
}
