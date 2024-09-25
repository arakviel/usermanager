using Microsoft.Extensions.DependencyInjection;
using UserManager.Domain.Contracts;
using UserManager.Domain.Services;

namespace UserManager.Domain;

public static class DomainDependencies
{
    public static void AddDomainDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IUserService, UserService>();
    }
}
