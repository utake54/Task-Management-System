using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.Task;

namespace TaskManagement.Database.Repository.Task
{
    public class TaskRepository : Repository<TaskMaster>, ITaskRepository
    {
        public TaskRepository(MasterDbContext context) : base(context)
        {
        }
    }
}
