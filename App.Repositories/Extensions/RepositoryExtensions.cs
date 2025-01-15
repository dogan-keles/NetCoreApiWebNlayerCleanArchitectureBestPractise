using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repositories.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionStrings = 
                configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
            options.UseNpgsql(connectionStrings!.PostgreSqlConnection, postgreSqlOptionsAction =>
                {
                    postgreSqlOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
                }
                );
        });
        return services;
    }
}

