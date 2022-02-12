using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PikoPlayer.Controls;
using PikoPlayer.Player;
using PikoPlayer.Themes;
using WPF.Core;

namespace PikoPlayer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            new ServiceCollectionBuilder()
                .AddMainWindow<PlayerView>()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IThemesRepository, ThemesRepository>();
                    services.AddSingleton<PlaybackControl>();
                    services.AddSingleton<RunOnStartupControl>();
                })
                .AddConfiguration("config.json")
                .AddViewModels()
                .Build();
            
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            Ioc.Default.GetRequiredService<PlayerView>().Show();
        }
    }
}
