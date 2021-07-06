using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace PikoPlayer.Themes
{
    public class ThemeListItem : ObservableObject
    {
        public string Name { get; set; }

        private bool _checked;
        public bool Checked
        {
            get => _checked;
            set => SetProperty(ref _checked, value);
        }
        public ICommand ChangeThemeCommand { get; set; }
    }
}