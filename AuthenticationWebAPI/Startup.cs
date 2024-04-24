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
using JwtAuthenticationManager;
using AuthenticationWebAPI.Persistence;
using AuthenticationWebAPI.Application.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using JwtAuthenticationManager;
using Newtonsoft.Json;

namespace AuthenticationWebAPI
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

            services.AddDbContextPool<UserDbContext>(builder =>
            {
                var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
                var dbName = Environment.GetEnvironmentVariable("DB_NAME");
                var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
                
                var connectionString = $"Server={dbHost};Database={dbName};User ID=sa;Password={dbPassword};Encrypt=False";
                //var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};Encrypt=False";

                builder.UseSqlServer(connectionString);
            });

            services.AddSingleton<JwtTokenHandler>();
            services.AddScoped<IUserRepository, UserRepository>();

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
