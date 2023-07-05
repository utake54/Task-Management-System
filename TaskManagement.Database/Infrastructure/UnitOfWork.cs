using TaskManagement.Database.Repository.OTP;
using TaskManagement.Database.Repository.Task;
using TaskManagement.Database.Repository.UserRepository;

namespace TaskManagement.Database.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IOTPRepository OTPRepository { get; }
        public ITaskRepository TaskRepository { get; }
        public IAssignTaskRepository AssignTaskRepository { get; }

        public UnitOfWork(IUserRepository userRepository, IOTPRepository oTPRepository,
                          ITaskRepository taskRepository, IAssignTaskRepository assignTaskRepository)
        {
            UserRepository = userRepository;
            OTPRepository = oTPRepository;
            TaskRepository = taskRepository;
            AssignTaskRepository = assignTaskRepository;
        }
    }
}
