using System.Windows;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace PikoPlayer.Player
{
    public partial class PlayerView : Window
    {
        public PlayerView()
        {
            InitializeComponent();

            var mainViewModel = Ioc.Default.GetRequiredService<PlayerViewModel>();
            DataContext = mainViewModel;

            Closing += mainViewModel.OnWindowClosing;

            var eventHandler = new WindowEventHandlers(this);
            PreviewMouseWheel += eventHandler.OnMouseScroll;
            MouseDown += eventHandler.OnMouseDown;
        }
    }
}
