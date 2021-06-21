using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<ThemeSettings> _themeSettings;
        private readonly IThemesRepository _themesRepository;
        private readonly PlaybackControlUtil _playbackControlUtil = new PlaybackControlUtil();

        public ICommand ControlPlayback => new RelayCommand<ControlAction>(action => _playbackControlUtil.ControlPlayback(action));
        public ICommand CloseCommand => new RelayCommand(() => Application.Current.Shutdown());
        public ObservableCollection<ThemeListItem> ThemesList { get; set; }

        private Theme _activeTheme;
        public Theme ActiveTheme
        {
            get => _activeTheme;
            set => SetProperty(ref _activeTheme, value);
        }

        public MainViewModel(IThemesRepository themesRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _themesRepository = themesRepository;
            ActiveTheme = _themesRepository.GetThemeByName(_configuration["Theme"]);

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
    }
}
