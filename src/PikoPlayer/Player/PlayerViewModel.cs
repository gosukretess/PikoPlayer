using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using Microsoft.Toolkit.Mvvm.Input;
using PikoPlayer.Config;
using PikoPlayer.Controls;
using PikoPlayer.Player.Models;
using PikoPlayer.Themes;
using WPF.Core;

namespace PikoPlayer.Player
{
    public class PlayerViewModel : ViewModel
    {
        private readonly IThemesRepository _themesRepository;
        private readonly PlaybackControl _playbackControl;
        private readonly RunOnStartupControl _runOnStartupControl;

        public ICommand ControlPlaybackCommand => new RelayCommand<PlaybackControlAction>(action => _playbackControl.ControlPlayback(action));
        public ICommand CloseCommand => new RelayCommand(() => Application.Current.Shutdown());
        public ICommand ResetPositionCommand => new RelayCommand(() =>
        {
            Position.X = 100;
            Position.Y = 100;
        });

        public ICommand RunOnStartupCommand => new RelayCommand(() =>
        {
            if (RunOnStartup)
            {
                var success = _runOnStartupControl.RemoveFromStartup();
                if (success) RunOnStartup = false;
            }
            else
            {
                var success = _runOnStartupControl.AddToStartup();
                if (success) RunOnStartup = true;
            }
        });


        public ObservableCollection<ThemeListItem> ThemesList { get; set; }

        private Theme _activeTheme;
        public Theme ActiveTheme
        {
            get => _activeTheme;
            set => SetProperty(ref _activeTheme, value);
        }

        private Dimensions _dimensions;
        public Dimensions Dimensions
        {
            get => _dimensions;
            set => SetProperty(ref _dimensions, value);
        }

        private Position _position;
        public Position Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        private bool _runOnStartup;
        public bool RunOnStartup
        {
            get => _runOnStartup;
            set => SetProperty(ref _runOnStartup, value);
        }

        public PlayerViewModel(IThemesRepository themesRepository, IConfiguration configuration,
            PlaybackControl playbackControl, RunOnStartupControl runOnStartupControl)
        {
            _themesRepository = themesRepository;
            _playbackControl = playbackControl;
            _runOnStartupControl = runOnStartupControl;
            Position = new Position
            {
                X = Convert.ToDouble(configuration["Position:X"]),
                Y = Convert.ToDouble(configuration["Position:Y"])

            };
            RunOnStartup = _runOnStartupControl.IsInStartup();
            ActiveTheme = _themesRepository.Get(configuration["Theme"]);
            Dimensions = new Dimensions
            {
                Scale = Convert.ToDouble(configuration["Scale"], CultureInfo.InvariantCulture)
            };

            PropagateThemesList(themesRepository);
        }

        public void ChangeTheme(string themeName)
        {
            ConfigurationSaveSystem.Change("Theme", themeName);
            ActiveTheme = _themesRepository.Get(themeName);
            foreach (var themeListItem in ThemesList)
            {
                themeListItem.Checked = themeListItem.Name.Equals(themeName);
            }
        }

        private void PropagateThemesList(IThemesRepository themesRepository)
        {
            ThemesList = new ObservableCollection<ThemeListItem>(themesRepository.GetThemesNames().Select(x =>
                new ThemeListItem
                {
                    Name = x,
                    Checked = ActiveTheme.Name == x,
                    ChangeThemeCommand = new RelayCommand(() => ChangeTheme(x))
                }));
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            ConfigurationSaveSystem.Change("Scale", Dimensions.Scale);
            ConfigurationSaveSystem.Change("Position", Position);
        }
    }
}
