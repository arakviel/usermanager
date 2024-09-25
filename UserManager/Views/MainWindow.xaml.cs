using System.Windows;
using UserManager.Presentation.ViewModels;

namespace UserManager.Presentation.Views;

public partial class MainWindow : Window
{
    public MainWindow(UserViewModel userViewModel)
    {
        InitializeComponent();
        DataContext = userViewModel;
    }
}