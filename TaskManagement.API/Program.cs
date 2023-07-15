using Hangfire;
using JWTAuth_Validation.Middleware;
using TaskManagement.API.Infrastructure.AutoMapper;
using TaskManagement.API.Infrastructure.Filters;
using TaskManagement.API.Infrastructure.Middleware;
using TaskManagement.API.Infrastructure.Services;
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
            });

            builder.Services.AddEndpointsApiExplorer()
                            .AddDbContext<MasterDbContext>()
                            .AddJWTAuthentication(builder.Configuration)
                            .SwaggerConfig()
                            .RepositoryAndService()
                            .AddAutoMapper(typeof(MapperProfile))
                            .AddHangfire(x => x.UseSqlServerStorage(string.Format(@"Data Source=LAPTOP-JF9VJ1L7\SQLEXPRESS;Initial Catalog=TaskManagement;Integrated Security=True;TrustServerCertificate=True;Encrypt=False")))
                            .AddHangfireServer();

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
            //app.UseMiddleware<ErrorHandlingMiddleware>();
            app.MapControllers();
            app.Run();
        }
    }
}