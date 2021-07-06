using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Extensions.Options;
using PikoPlayer.Config;
using PikoPlayer.ViewModels;

namespace PikoPlayer
{
    public partial class MainWindow : Window
    {
        private readonly PositionSettings _positionSettings;

        public MainWindow(MainViewModel viewModel, IOptions<PositionSettings> positionSettings)
        {
            InitializeComponent();
            DataContext = viewModel;
            _positionSettings = positionSettings.Value;

            ShowInTaskbar = false;

            Closing += viewModel.OnWindowClosing;
            var eventHandler = new WindowEventHandlers(this);
            PreviewMouseWheel += eventHandler.OnMouseScroll;
            MouseDown += eventHandler.OnMouseDown;
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
    }
}
