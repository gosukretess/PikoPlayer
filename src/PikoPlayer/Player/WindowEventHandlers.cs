using System.Windows;
using System.Windows.Input;
using PikoPlayer.Controls;

namespace PikoPlayer.Player
{
    public class WindowEventHandlers
    {
        private readonly Window _window;
        private readonly VolumeControl _volumeController;

        public WindowEventHandlers(Window window)
        {
            _window = window;
            _volumeController = new VolumeControl(window);
        }

        public void OnMouseScroll(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                _volumeController.VolumeUp();
            else if (e.Delta < 0)
                _volumeController.VolumeDown();
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
