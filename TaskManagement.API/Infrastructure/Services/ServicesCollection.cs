using TaskManagement.Database.Infrastructure;
using TaskManagement.Database.Repository.OTP;
using TaskManagement.Database.Repository.Task;
using TaskManagement.Database.Repository.UserRepository;
using TaskManagement.Service.OTPService;
using TaskManagement.Service.TaskService;
using TaskManagement.Service.UserService;
using TaskManagement.Utility.Email;
using TaskManagement.Utility;
using TaskManagement.Service.OverDueService;
using TaskManagement.Database.Repository.EmailTemplate;
using TaskManagement.Database.Repository.Category;
using TaskManagement.Service.CategoryService;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TaskManagement.Service.Profile;
using TaskManagement.Service.Reports;
using FluentValidation;
using TaskManagement.API.Infrastructure.Validator.Login;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;

namespace TaskManagement.API.Infrastructure.Services
{
    public static class ServicesCollection
    {
        [Obsolete]
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
                .AddTransient<IProfileService, ProfileService>()
                .AddTransient<IReportService, ReportService>()
                .AddValidatorsFromAssemblyContaining<UserLoginRequestValidator>()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserLoginRequestValidator>())




.AddValidatorsFromAssemblyContaining<UserLoginRequestValidator>()



.AddValidatorsFromAssemblyContaining(typeof(UserLoginRequestValidator))


.AddValidatorsFromAssembly(typeof(UserLoginRequestValidator).Assembly)


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
