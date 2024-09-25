using Microsoft.EntityFrameworkCore;
using UserManager.Core;

namespace UserManager.Infrastructure.Persistence;

internal class UserManagerContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UserManagerContext(DbContextOptions<UserManagerContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasData(SeedUsers());
    }

    private List<User> SeedUsers()
    {
        List<User> users = new List<User>();

        for (int i = 1; i <= 10; i++)
        {
            users.Add(new User
            {
                Id = Guid.NewGuid(),
                Name = $"User {i}",
                Age = 20 + i // Вік від 21 до 30
            });
        }
        return users;
    }
}
