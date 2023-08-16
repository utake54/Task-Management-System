using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Model.Model.Task.DTO
{
    public class AllTaskDTO
    {
        public IEnumerable<TaskExportDTO> Tasks { get; set; }
        public int TotalRecords { get; set; }
    }
}
