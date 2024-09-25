using UserManager.Core;

namespace UserManager.Infrastructure.Persistence.Contracts;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByNameAsync(string name);
}
