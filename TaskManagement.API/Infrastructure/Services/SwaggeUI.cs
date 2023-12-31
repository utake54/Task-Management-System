﻿using Microsoft.OpenApi.Models;
using System.Reflection;

namespace TaskManagement.API.Infrastructure.Services
{
    public static class SwaggeUI
    {
        public static IServiceCollection SwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManagement.API", Version = "v1" });
                 c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                 {
                     In = ParameterLocation.Header,

                     Description = "Please insert TOken",
                     Name = "Authorize",
                     Type = SecuritySchemeType.Http,
                     BearerFormat = "JWT",
                     Scheme = "bearer"

                 });

                 //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                 //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                 //c.IncludeXmlComments(xmlPath);


                 c.AddSecurityRequirement(new OpenApiSecurityRequirement {
             {
                 new OpenApiSecurityScheme
                 {
                     Reference=new OpenApiReference
                     {
                         Type=ReferenceType.SecurityScheme,
                         Id="Bearer"
                     }
                 },
                 new string[]{}
                    }
             });


             });

            return services;
        }
    }
}
