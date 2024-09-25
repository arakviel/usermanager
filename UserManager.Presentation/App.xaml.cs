using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;
using UserManager.Domain;
using UserManager.Infrastructure;
using UserManager.Presentation;
using UserManager.Presentation.Views;

namespace UserManager;

public partial class App : Application
{
    private ServiceProvider _serviceProvider;
    private IConfiguration _configuration;


    public App()
    {
        var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);

        _configuration = builder.Build();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddInfrastructureDependencies(_configuration);
        services.AddDomainDependencies();
        services.AddPresentationDependencies();
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetService<MainWindow>()!;
        mainWindow.Show();
    }
}