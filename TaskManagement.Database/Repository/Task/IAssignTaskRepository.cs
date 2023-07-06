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
    public interface IAssignTaskRepository : IRepository<AssignTask>
    {

    }
}
