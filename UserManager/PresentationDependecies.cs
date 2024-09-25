using Microsoft.Extensions.DependencyInjection;
using UserManager.Presentation.ViewModels;
using UserManager.Presentation.Views;

namespace UserManager.Presentation;

public static class PresentationDependecies
{
    public static void AddPresentationDependencies(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>()
            .AddSingleton<UserViewModel>();
    }
}
