﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Database.Repository.Category;
using TaskManagement.Database.Repository.EmailTemplate;
using TaskManagement.Database.Repository.OTP;
using TaskManagement.Database.Repository.Task;
using TaskManagement.Database.Repository.UserRepository;

namespace TaskManagement.Database.Infrastructure
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        MasterDbContext MasterDbContext { get; }
        public IUserRepository UserRepository { get; }
        public IOTPRepository OTPRepository { get; }
        public ITaskRepository TaskRepository { get; }
        public IAssignTaskRepository AssignTaskRepository { get; }
        public IEmailTemplateRepository EmailTemplateRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
    }
}
