
using AutoMapper;
using Hangfire;
using JWTAuth_Validation.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TaskManagement.API.Infrastructure.AutoMapper;
using TaskManagement.API.Infrastructure.Filters;
using TaskManagement.API.Infrastructure.Services;
using TaskManagement.Database;
using TaskManagement.Database.Infrastructure;
using TaskManagement.Database.Repository.OTP;
using TaskManagement.Database.Repository.Task;
using TaskManagement.Database.Repository.UserRepository;
using TaskManagement.Service.OTPService;
using TaskManagement.Service.TaskService;
using TaskManagement.Service.UserService;
using TaskManagement.Utility;
using TaskManagement.Utility.Email;

namespace TaskManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(config =>
            {
                config.Filters.Add<ModelStateFilter>();
                config.Filters.Add<ResultFilter>();
                config.Filters.Add<GlobalExceptionFilter>();
            });
            builder.Services.AddEndpointsApiExplorer()
                            .AddDbContext<MasterDbContext>()
                            .AddJWTAuthentication()
                            .SwaggerConfig()
                            .RepositoryAndService()
                            .AddAutoMapper(typeof(MapperProfile))
                            .AddHangfire(x => x.UseSqlServerStorage(string.Format(@"Data Source=LAPTOP-JF9VJ1L7\SQLEXPRESS;Initial Catalog=TaskManagement;Integrated Security=True;TrustServerCertificate=True;Encrypt=False")))
                            .AddHangfireServer();

            // Configure the HTTP request pipeline.

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHangfireDashboard("/HangDashboard");
            app.UseMiddleware<JWTMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}