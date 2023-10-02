using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.Server;
using JWTAuth_Validation.Middleware;
using TaskManagement.API.Infrastructure.AutoMapper;
using TaskManagement.API.Infrastructure.Filters;
using TaskManagement.API.Infrastructure.Services;
using TaskManagement.API.Infrastructure.Validator.Login;
using TaskManagement.Database;

namespace TaskManagement.API
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Filters
            builder.Services.AddControllers(config =>
            {
                config.Filters.Add<ModelStateFilter>();
                config.Filters.Add<ResultFilter>();
                config.Filters.Add<GlobalExceptionFilter>();
                config.ValidateComplexTypesIfChildValidationFails = true;
                //config.Filters.Add<Permissible>();
            });

            //Services
            builder.Services.AddEndpointsApiExplorer()
                            .AddDbContext<MasterDbContext>()
                            .AddJWTAuthentication(builder.Configuration)
                            .SwaggerConfig()
                            .RepositoryAndService()
                            .AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnectionString")))
                            .AddHangfireServer()
                            .AddRecurringJobManager()
                            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());


            //Redis 
            builder.Services.AddStackExchangeRedisCache(redisoption =>
            {
                string connection = builder.Configuration.GetConnectionString("Redis");

                redisoption.Configuration = connection;
            });

            //MiddleWares
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