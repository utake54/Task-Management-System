using TaskManagement.Database.Infrastructure;
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
                .AddTransient<IAssignTaskRepository, AssignTaskRepository>();

            return services;

        }
    }
}
