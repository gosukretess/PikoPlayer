using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace PikoPlayer.Controls
{
    public class VolumeControl
    {
        private readonly Window _window;
        private const int AppCommandVolumeUp = 0xA0000;
        private const int AppCommandVolumeDown = 0x90000;
        private const int WmAppCommand = 0x319;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int msg,
            IntPtr wParam, IntPtr lParam);

        public VolumeControl(Window window)
        {
            _window = window;
        }

        public void VolumeDown()
        {
            var handle = new WindowInteropHelper(_window).Handle;
            SendMessageW(handle, WmAppCommand, handle, (IntPtr)AppCommandVolumeDown);
        }

        public void VolumeUp()
        {
            var handle = new WindowInteropHelper(_window).Handle;
            SendMessageW(handle, WmAppCommand, handle, (IntPtr)AppCommandVolumeUp);
        }
    }
}