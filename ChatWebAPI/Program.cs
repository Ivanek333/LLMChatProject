
using ChatWebAPI.Persistence;
using Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatWebAPI
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            var webHost = CreateHostBuilder(args).Build();

            await ApplyMigrations(webHost.Services);

            await webHost.RunAsync();
        }

        private static async Task ApplyMigrations(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await using ChatDbContext dbContext = scope.ServiceProvider.GetRequiredService<ChatDbContext>();
            await dbContext.Database.MigrateAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
