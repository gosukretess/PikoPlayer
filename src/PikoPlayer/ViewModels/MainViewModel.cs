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
        private readonly IConfiguration _configuration;
        private readonly IThemesRepository _themesRepository;
        private readonly PlaybackControlUtil _playbackControlUtil = new PlaybackControlUtil();

        public ICommand ControlPlayback => new RelayCommand<ControlAction>(action => _playbackControlUtil.ControlPlayback(action));
        public ICommand ScaleCommand => new RelayCommand<string>(direction =>
        {
            switch (direction)
            {
                case "Up":
                    Dimensions.Scale += 0.05;
                    break;
                case "Down":
                    Dimensions.Scale -= 0.05;
                    break;
            }
            
        });
        public ICommand CloseCommand => new RelayCommand(() => Application.Current.Shutdown());
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

        public MainViewModel(IThemesRepository themesRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _themesRepository = themesRepository;
            ActiveTheme = _themesRepository.GetThemeByName(_configuration["Theme"]);
            Dimensions = new Dimensions
            {
                Scale = Convert.ToDouble(_configuration["Scale"], CultureInfo.InvariantCulture)
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
        }
    }
}
