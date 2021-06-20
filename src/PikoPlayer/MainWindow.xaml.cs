using System.Windows;
using System.Windows.Input;
using PikoPlayer.Controls;
using PikoPlayer.ViewModels;

namespace PikoPlayer
{
    public partial class MainWindow : Window
    {
        private readonly VolumeControlUtil _volumeController;
        
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            ShowInTaskbar = false;
            _volumeController = new VolumeControlUtil(this);
        }

        private void Window_MoveEvent(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_ScrollEvent(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                _volumeController.VolUp();
            else if (e.Delta < 0)
                _volumeController.VolDown();
        }
    }
}
