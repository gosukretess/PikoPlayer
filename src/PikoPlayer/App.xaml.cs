using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PikoPlayer.Controls;
using PikoPlayer.Themes;
using PikoPlayer.ViewModels;

namespace PikoPlayer
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ShutdownMode = ShutdownMode.OnMainWindowClose;

            Configuration = BuildConfiguration();
            ServiceProvider = ConfigureServices();
            
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("config.json", optional: false, reloadOnChange: true)
                .Build();
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton(Configuration);

            services.AddSingleton<IThemesRepository, ThemesRepository>();
            services.AddSingleton<PlaybackControlUtil>();
            services.AddSingleton<RunOnStartupSetter>();

            services.AddTransient<MainWindow>();
            services.AddTransient<MainViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
