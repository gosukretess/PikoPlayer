using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace PikoPlayer.ViewModels
{
    public class Position : ObservableObject
    {
        private double _x;
        public double X
        {
            get => _x;
            set => SetProperty(ref _x, value);
        }

        private double _y;
        public double Y
        {
            get => _y;
            set => SetProperty(ref _y, value);
        }
    }
}
