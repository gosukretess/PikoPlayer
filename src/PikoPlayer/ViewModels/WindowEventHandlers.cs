using System.Windows;
using System.Windows.Input;
using PikoPlayer.Controls;

namespace PikoPlayer.ViewModels
{
    public class WindowEventHandlers
    {
        private readonly Window _window;
        private readonly VolumeControlUtil _volumeController;

        public WindowEventHandlers(Window window)
        {
            _window = window;
            _volumeController = new VolumeControlUtil(window);
        }

        public void OnMouseScroll(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                _volumeController.VolUp();
            else if (e.Delta < 0)
                _volumeController.VolDown();
        }

        public void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                _window.DragMove();
            }
        }
    }
}
