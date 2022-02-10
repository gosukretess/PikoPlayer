using System;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace PikoPlayer.Player.Models
{
    public class Dimensions : ObservableObject
    {
        private double _scale;
        public double Scale
        {
            get => _scale;
            set
            {
                SetProperty(ref _scale, Math.Round(value, 2));
                WindowWidth = 96 * _scale;
                WindowHeight = 24 * _scale;
                ButtonWidth = 18 * _scale;
                ButtonHeight = 18 * _scale;
                WindowMargin = 3 * _scale;
                HamburgerSize = 12 * _scale;
                CentralButtonMargin = new Thickness(9 * _scale, 0, 9 * _scale, 0);
            }
        }

        private double _windowHeight;
        public double WindowHeight
        {
            get => _windowHeight;
            set => SetProperty(ref _windowHeight, value);
        }

        private double _windowWidth;
        public double WindowWidth
        {
            get => _windowWidth;
            set => SetProperty(ref _windowWidth, value);
        }

        private double _buttonWidth;
        public double ButtonWidth
        {
            get => _buttonWidth;
            set => SetProperty(ref _buttonWidth, value);
        }

        private double _buttonHeight;
        public double ButtonHeight
        {
            get => _buttonHeight;
            set => SetProperty(ref _buttonHeight, value);
        }
        
        private double _windowMargin;
        public double WindowMargin
        {
            get => _windowMargin;
            set => SetProperty(ref _windowMargin, value);
        }

        private double _hamburgerSize;
        public double HamburgerSize
        {
            get => _hamburgerSize;
            set => SetProperty(ref _hamburgerSize, value);
        } 
        
        private Thickness _centralButtonMargin;
        public Thickness CentralButtonMargin
        {
            get => _centralButtonMargin;
            set => SetProperty(ref _centralButtonMargin, value);
        }
    }
}