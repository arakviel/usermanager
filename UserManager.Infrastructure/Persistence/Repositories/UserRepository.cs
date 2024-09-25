using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using UserManager.Core;
using UserManager.Infrastructure.Persistence.Contracts;

namespace UserManager.Infrastructure.Persistence.Repositories;

internal class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly UserManagerContext _context;

    public UserRepository(UserManagerContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetByNameAsync(string name)
    {
        return await _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Name == name);
    }

    protected override SetPropertyCalls<User> SettersInit(SetPropertyCalls<User> setters, User user)
    {
        return setters.SetProperty(u => u.Name, user.Name)
            .SetProperty(u => u.Age, user.Age);
    }
}
