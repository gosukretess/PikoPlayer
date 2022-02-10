using System;
using System.Linq;
using Microsoft.Win32;

namespace PikoPlayer.Controls
{
    public class RunOnStartupControl
    {
        private const string RegistryKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private readonly string _applicationName = typeof(App).Assembly.GetName().Name;
        private readonly string _executablePath = typeof(App).Assembly.Location;

        public bool AddToStartup()
        {
            try
            {
                var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath, true);
                key.SetValue(_applicationName, _executablePath);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool RemoveFromStartup()
        {
            try
            {
                var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath, true);
                if (key.GetValue(_applicationName).ToString().Equals(_executablePath, StringComparison.InvariantCultureIgnoreCase))
                {
                    key.DeleteValue(_applicationName);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool IsInStartup()
        {
            try
            {
                var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath, false);
                if (key.GetValueNames().Contains(_applicationName))
                {
                    var value = key.GetValue(_applicationName).ToString();
                    return value.ToLower().Equals(_executablePath.ToLower());
                }
            }
            catch (Exception)
            {
                // log key not found
            }

            return false;
        }
    }
}