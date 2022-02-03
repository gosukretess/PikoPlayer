using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace PikoPlayer
{
    public class RunOnStartupSetter
    {
        private readonly string _applicationName = "PikoPlayer";
        private readonly string _executablePath = Path.Combine(AppContext.BaseDirectory, "PikoPlayer.exe");

        public bool AddToStartup()
        {
            try
            {
                var rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                rk.SetValue(_applicationName, _executablePath);
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
                var rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (rk.GetValue(_applicationName).ToString().Equals(_executablePath, StringComparison.InvariantCultureIgnoreCase))
                {
                    rk.DeleteValue(_applicationName);
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
                var rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", false);
                if (rk.GetValueNames().Contains(_applicationName))
                {
                    var value = rk.GetValue(_applicationName).ToString();
                    return value.ToLower().Equals(_executablePath.ToLower());
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}