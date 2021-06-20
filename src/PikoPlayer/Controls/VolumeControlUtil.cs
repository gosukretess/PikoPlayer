using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace PikoPlayer.Controls
{
    public class VolumeControlUtil
    {
        private readonly Window _window;
        private const int AppCommandVolumeMute = 0x80000;
        private const int AppCommandVolumeUp = 0xA0000;
        private const int AppCommandVolumeDown = 0x90000;
        private const int WmAppCommand = 0x319;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int msg,
            IntPtr wParam, IntPtr lParam);

        public VolumeControlUtil(Window window)
        {
            _window = window;
        }

        // Unused
        public void Mute()
        {
            var handle = new WindowInteropHelper(_window).Handle;
            SendMessageW(handle, WmAppCommand, handle,
                (IntPtr)AppCommandVolumeMute);
        }

        public void VolDown()
        {
            var handle = new WindowInteropHelper(_window).Handle;
            SendMessageW(handle, WmAppCommand, handle,
                (IntPtr)AppCommandVolumeDown);
        }

        public void VolUp()
        {
            var handle = new WindowInteropHelper(_window).Handle;
            SendMessageW(handle, WmAppCommand, handle,
                (IntPtr)AppCommandVolumeUp);
        }
    }
}