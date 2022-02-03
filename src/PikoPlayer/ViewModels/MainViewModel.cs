using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PikoPlayer.Config;
using PikoPlayer.Controls;
using PikoPlayer.Themes;

namespace PikoPlayer.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IThemesRepository _themesRepository;
        private readonly PlaybackControlUtil _playbackControlUtil;
        private readonly RunOnStartupSetter _runOnStartupSetter;

        public ICommand ControlPlaybackCommand => new RelayCommand<ControlAction>(action => _playbackControlUtil.ControlPlayback(action));
        public ICommand CloseCommand => new RelayCommand(() => Application.Current.Shutdown());
        public ICommand ResetPositionCommand => new RelayCommand(() =>
        {
            Position.X = 100;
            Position.Y = 100;
        });

        public ICommand RunOnStartupCommand => new RelayCommand(() =>
        {
            if (RunOnStartup == true)
            {
                // disable
                _runOnStartupSetter.RemoveFromStartup();
                RunOnStartup = false;
            }
            else
            {
                // enable
                _runOnStartupSetter.AddToStartup();
                RunOnStartup = true;
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

        public MainViewModel(IThemesRepository themesRepository, IConfiguration configuration, 
            PlaybackControlUtil playbackControlUtil, RunOnStartupSetter runOnStartupSetter)
        {
            _themesRepository = themesRepository;
            _playbackControlUtil = playbackControlUtil;
            _runOnStartupSetter = runOnStartupSetter;
            Position = new Position
            {
                X = Convert.ToDouble(configuration["Position:X"]),
                Y = Convert.ToDouble(configuration["Position:Y"])

            };
            RunOnStartup = _runOnStartupSetter.IsInStartup();
            ActiveTheme = _themesRepository.GetThemeByName(configuration["Theme"]);
            Dimensions = new Dimensions
            {
                Scale = Convert.ToDouble(configuration["Scale"], CultureInfo.InvariantCulture)
            };

            PropagateThemesList(themesRepository);
        }

        public void ChangeTheme(string themeName)
        {
            ConfigurationSaveSystem.Change("Theme", themeName);
            ActiveTheme = _themesRepository.GetThemeByName(themeName);
            foreach (var themeListItem in ThemesList)
            {
                themeListItem.Checked = themeListItem.Name.Equals(themeName);
            }
        }

        private void PropagateThemesList(IThemesRepository themesRepository)
        {
            ThemesList = new ObservableCollection<ThemeListItem>(themesRepository.GetThemesList().Select(x =>
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
