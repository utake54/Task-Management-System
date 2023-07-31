﻿using TaskManagement.Database.Infrastructure;
using TaskManagement.Database.Repository.OTP;
using TaskManagement.Database.Repository.Task;
using TaskManagement.Database.Repository.UserRepository;
using TaskManagement.Service.OTPService;
using TaskManagement.Service.TaskService;
using TaskManagement.Service.UserService;
using TaskManagement.Utility.Email;
using TaskManagement.Utility;
using TaskManagement.API.Infrastructure.Filters;
using System.Web.Http.Filters;
using TaskManagement.API.Reminders;
using Hangfire;
using Hangfire.MemoryStorage;
using TaskManagement.Service.OverDueService;
using TaskManagement.Database.Repository.EmailTemplate;
using TaskManagement.Database.Repository.Category;
using TaskManagement.Service.CategoryService;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Configuration;

namespace TaskManagement.API.Infrastructure.Services
{
    public static class ServicesCollection
    {
        public static IServiceCollection RepositoryAndService(this IServiceCollection services)
        {
            services
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IOTPRepository, OTPRepository>()
                .AddTransient<IOTPService, OTPService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<ISendMail, SendEmail>()
                .AddTransient<IAppSettings, AppSettings>()
                .AddTransient<ITaskRepository, TaskRepository>()
                .AddTransient<ITaskService, TaskService>()
                .AddTransient<IAssignTaskRepository, AssignTaskRepository>()
                .AddTransient<IOverdueService, OverdueService>()
                .AddTransient<IEmailTemplateRepository, EmailTemplateRepository>()
                .AddTransient<ICategoryRepository, CategoryRepository>()
                .AddTransient<ICategoryService, CategoryService>()
             .AddTransient<IConfiguration>(sp =>
                {
                    IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                    configurationBuilder.AddJsonFile("appsettings.json");
                    return configurationBuilder.Build();
                })
                .TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            return services;
        }
    }
}
