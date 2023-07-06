using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.Task;
using TaskManagement.Model.Model.Task.Request;

namespace TaskManagement.Database.Repository.Task
{
    public class TaskRepository : Repository<TaskMaster>, ITaskRepository
    {
        public TaskRepository(MasterDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AssignTask>> GetMyTask(int userId)
        {
            var task = await (from tm in Context.TaskMaster
                              join at in Context.AssignedTask
                              on tm.Id equals at.TaskId
                              where at.UserId == userId && tm.IsActive == true
                              select at).ToListAsync();

            return task;
        }
    }
}
