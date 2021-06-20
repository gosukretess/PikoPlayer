using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PikoPlayer.Controls;
using PikoPlayer.Themes;

namespace PikoPlayer.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly PlaybackControlUtil _playbackControlUtil = new PlaybackControlUtil();
        public ICommand ControlPlayback => new RelayCommand<ControlAction>(action => _playbackControlUtil.ControlPlayback(action));
        public ICommand CloseCommand => new RelayCommand(() => Application.Current.Shutdown());
        public IThemesRepository ThemesRepository { get; }

        public MainViewModel(IThemesRepository themesRepository)
        {
            ThemesRepository = themesRepository;
        }
    }
}
