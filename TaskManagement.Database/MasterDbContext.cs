﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Model.Model.Category;
using TaskManagement.Model.Model.Email;
using TaskManagement.Model.Model.OTP;
using TaskManagement.Model.Model.Task;
using TaskManagement.Model.Model.Task.Request;
using TaskManagement.Model.Model.User;

namespace TaskManagement.Database
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-JF9VJ1L7\\SQLEXPRESS;Initial Catalog=TaskManagement;Integrated Security=True;TrustServerCertificate=True;Encrypt=False");
            }
        }
        public DbSet<UserMaster> UserMaster { get; set; }
        public DbSet<TaskMaster> TaskMaster { get; set; }
        public DbSet<TeamMaster> TeamMaster { get; set; }
        public DbSet<OTPMaster> OTPMaster { get; set; }
        public DbSet<AssignTask> AssignedTask { get; set; }
        public DbSet<RoleMaster> RoleMaster { get; set; }
        public DbSet<EmailTemplateMaster> EmailTemplateMaster { get; set; }
        public DbSet<TaskCategoryMaster> TaskCategoryMaster { get; set; }
    }
}
