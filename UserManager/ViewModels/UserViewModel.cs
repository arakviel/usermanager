using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using UserManager.Core;
using UserManager.Domain.Contracts;
using UserManager.Domain.Dto.User;
using UserManager.Presentation.Helpers;

namespace UserManager.Presentation.ViewModels;

public class UserViewModel : INotifyPropertyChanged
{
    private readonly IUserService _userService;
    private User _selectedUser;
    public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

    public string Name { get; set; }
    public int Age { get; set; }

    public User SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged();
            OnSelectedUserChanged();
        }
    }

    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand LoadUsersCommand { get; }

    public UserViewModel(IUserService userService)
    {
        _userService = userService;

        AddCommand = new RelayCommand(async _ => await CreateUserAsync());
        UpdateCommand = new RelayCommand(async _ => await UpdateUserAsync(), _ => SelectedUser != null);
        DeleteCommand = new RelayCommand(async _ => await DeleteUserAsync(), _ => SelectedUser != null);
        LoadUsersCommand = new RelayCommand(async _ => await LoadUsersAsync());

        // Load users on startup
        LoadUsersCommand.Execute(null);
    }

    private async Task LoadUsersAsync()
    {
        var users = await _userService.GetAllUsersAsync();
        Users.Clear();
        foreach (var user in users)
        {
            Users.Add(user);
        }
    }

    private async Task CreateUserAsync()
    {
        var newUser = new CreateUserDto { Name = Name, Age = Age };
        await _userService.CreateUserAsync(newUser);
        await LoadUsersAsync(); // Refresh list
    }

    private async Task UpdateUserAsync()
    {
        if (SelectedUser != null)
        {
            var updatedUser = new UpdateUserDto { Id = SelectedUser.Id, Name = SelectedUser.Name, Age = SelectedUser.Age };
            await _userService.UpdateUserAsync(updatedUser);
            await LoadUsersAsync(); // Refresh list
        }
    }

    private async Task DeleteUserAsync()
    {
        if (SelectedUser != null)
        {
            await _userService.DeleteUserAsync(SelectedUser.Id);
            await LoadUsersAsync(); // Refresh list
        }
    }

    private void OnSelectedUserChanged()
    {
        if (SelectedUser != null)
        {
            Name = SelectedUser.Name;
            Age = SelectedUser.Age;
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Age));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
