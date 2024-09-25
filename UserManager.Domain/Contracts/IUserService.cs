using UserManager.Core;
using UserManager.Domain.Dto.User;

namespace UserManager.Domain.Contracts;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(Guid id);
    Task CreateUserAsync(CreateUserDto createUserDto);
    Task UpdateUserAsync(UpdateUserDto updateUserDto);
    Task DeleteUserAsync(Guid id);
}
