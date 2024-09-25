using UserManager.Core;
using UserManager.Domain.Contracts;
using UserManager.Domain.Dto.User;
using UserManager.Infrastructure.Persistence.Contracts;

namespace UserManager.Domain.Services;

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = createUserDto.Name,
            Age = createUserDto.Age
        };
        await _userRepository.AddAsync(user);
    }

    public async Task UpdateUserAsync(UpdateUserDto updateUserDto)
    {
        var user = new User();
        user.Name = updateUserDto.Name;
        user.Age = updateUserDto.Age ?? 0;
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await _userRepository.DeleteAsync(id);
    }
}
