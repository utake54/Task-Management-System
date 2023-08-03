using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.SearchModel;
using TaskManagement.Model.Model.Task;
using TaskManagement.Model.Model.Task.DTO;
using TaskManagement.Model.Model.Task.Request;

namespace TaskManagement.Database.Repository.Task
{
    public interface ITaskRepository : IRepository<TaskMaster>
    {
        Task<IEnumerable<MyTaskDTO>> GetMyTask(int userId);
        Task<IEnumerable<TaskExportDTO>> GetAllTask(int companyId, SearchModel searchModel);
        Task<List<TaskExportDTO>> GetTaskDetails(int companyId);
    }
}
