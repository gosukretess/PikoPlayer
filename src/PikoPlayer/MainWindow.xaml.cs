using System.Windows;
using PikoPlayer.ViewModels;

namespace PikoPlayer
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            ShowInTaskbar = false;

            Closing += viewModel.OnWindowClosing;
            var eventHandler = new WindowEventHandlers(this);
            PreviewMouseWheel += eventHandler.OnMouseScroll;
            MouseDown += eventHandler.OnMouseDown;
        }
    }
}
