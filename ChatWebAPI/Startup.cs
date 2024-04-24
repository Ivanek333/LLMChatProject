using ChatWebAPI.Application.Abstractions;
using ChatWebAPI.Persistence;
using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Shared.Application.Behaviors;
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Middleware;
using ChatWebAPI.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using JwtAuthenticationManager;
using Newtonsoft.Json;

namespace ChatWebAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCustomJwtAuthentication();
            services.AddCustomJwtAuthorization();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContextPool<ChatDbContext>(builder =>
            {
                var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
                var dbName = Environment.GetEnvironmentVariable("DB_NAME");
                var dbPassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");
                var connectionString = $"server={dbHost};port=3306;database={dbName};user=root;password={dbPassword}";

                builder.UseMySQL(connectionString);
            });

            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<ILLMBackgroundService, GPTBackroundService>();
            services.AddScoped<ILLMBackgroundService, SLMBackgroundService>();
            //services.AddHttpClient<ILLMBackgroundService>();

            services.AddTransient<ExceptionInterceptorMiddleware>();

            services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionInterceptorMiddleware>();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
