using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.Options;
using PikoPlayer.Config;
using PikoPlayer.Controls;
using PikoPlayer.ViewModels;

namespace PikoPlayer
{
    public partial class MainWindow : Window
    {
        private readonly VolumeControlUtil _volumeController;
        private readonly PositionSettings _positionSettings;

        public MainWindow(MainViewModel viewModel, IOptions<PositionSettings> positionSettings)
        {
            InitializeComponent();
            DataContext = viewModel;
            _positionSettings = positionSettings.Value;

            ShowInTaskbar = false;
            _volumeController = new VolumeControlUtil(this);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _positionSettings.Y = Top;
            _positionSettings.X = Left;
            ConfigurationSaveSystem.Change("Position", _positionSettings);
            base.OnClosing(e);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            Top = _positionSettings.Y;
            Left = _positionSettings.X;
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
