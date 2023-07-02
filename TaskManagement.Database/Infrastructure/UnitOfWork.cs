using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Repository.OTP;
using TaskManagement.Database.Repository.UserRepository;

namespace TaskManagement.Database.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IOTPRepository OTPRepository { get; }
        public UnitOfWork(IUserRepository userRepository, IOTPRepository oTPRepository)
        {
            UserRepository = userRepository;
            OTPRepository = oTPRepository;
        }
    }
}
