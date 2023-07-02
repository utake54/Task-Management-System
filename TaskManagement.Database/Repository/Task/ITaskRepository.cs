using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.Task;

namespace TaskManagement.Database.Repository.Task
{
    public interface ITaskRepository : IRepository<TaskMaster>
    {
    }
}
