using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Model.Model.User;

namespace TaskManagement.Database.Repository.UserRepository
{
    public interface IUserRepository:IRepository<UserMaster>
    {

    }
}
