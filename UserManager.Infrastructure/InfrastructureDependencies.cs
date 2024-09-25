using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManager.Infrastructure.Persistence;
using UserManager.Infrastructure.Persistence.Contracts;
using UserManager.Infrastructure.Persistence.Repositories;

namespace UserManager.Infrastructure;

public static class InfrastructureDependencies
{
    public static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserManagerContext>(o => o.UseSqlite(configuration.GetConnectionString(nameof(UserManagerContext))))
            .AddSingleton<IUserRepository, UserRepository>();
    }
}
