using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.Server;
using JWTAuth_Validation.Middleware;
using TaskManagement.API.Infrastructure.AutoMapper;
using TaskManagement.API.Infrastructure.Filters;
using TaskManagement.API.Infrastructure.Services;
using TaskManagement.API.Reminders;
using TaskManagement.Database;

namespace TaskManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(config =>
            {
                config.Filters.Add<ModelStateFilter>();
                config.Filters.Add<ResultFilter>();
                config.Filters.Add<GlobalExceptionFilter>();
                config.Filters.Add<Permissible>();
            });

            builder.Services.AddEndpointsApiExplorer()
                            .AddDbContext<MasterDbContext>()
                            .AddJWTAuthentication(builder.Configuration)
                            .SwaggerConfig()
                            .RepositoryAndService()
                            .AddAutoMapper(typeof(MapperProfile))
                            .AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnectionString")))
                            .AddHangfireServer()
                            .AddRecurringJobManager();

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