using Microsoft.EntityFrameworkCore;
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
    public class TaskRepository : Repository<TaskMaster>, ITaskRepository
    {
        public TaskRepository(MasterDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskMaster>> GetAllTask(int companyId, SearchModel searchModel)
        {
            
            var taskList = await Context.TaskMaster.Where(x => x.CompanyId == companyId)
                                .Take(searchModel.PageSize)
                                .Skip((searchModel.PageNumber - 1) * searchModel.PageSize)
                                .OrderBy(x => x.Id)
                                .ToListAsync();
            return taskList;
        }

        public async Task<IEnumerable<MyTaskDTO>> GetMyTask(int userId)
        {
            var task = await (from tm in Context.TaskMaster
                              join at in Context.AssignedTask
                              on tm.Id equals at.TaskId
                              join um in Context.UserMaster on at.AssignedBy equals um.Id
                              where at.UserId == userId && tm.IsActive == true

                              select new MyTaskDTO
                              {
                                  Id = tm.Id,
                                  Task = tm.Title,
                                  Description = tm.Description,
                                  Priority = tm.Priority,
                                  AssignedDate = at.AssignedDate.ToString("dd-MM-yyyy"),
                                  DueDate = tm.DueDate.ToString("dd-MM-yyyy"),
                                  CompletedDate = at.EndDate.ToString("dd-MM-yyyy"),
                                  AssignedBy = um.FirstName
                              }).ToListAsync();
            return task;
        }
    }
}
