using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Repository.UserRepository;

namespace TaskManagement.Database.Infrastructure
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
    }
}
