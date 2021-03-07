using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace PaymentGateway.API.Extensions
{
    public static class WebHostExtensions
    {
        public static IHost MigrateDbContext<TContext>(
            this IHost webHost,
            Action<TContext> seeder)
            where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);
                    InvokeSeeder(seeder, context);
                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
                }
            }

            return webHost;
        }

        private static void InvokeSeeder<TContext>(
            Action<TContext> seeder,
            TContext context)
            where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context);
        }
    }
}
