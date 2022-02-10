using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace PikoPlayer.Themes
{
    public class ThemesRepository : IThemesRepository
    {
        private readonly Dictionary<string, Theme> _themes = new Dictionary<string, Theme>();

        public ThemesRepository()
        {
            var themesDirectory = Path.Combine(AppContext.BaseDirectory, "Resources", "Themes");
            foreach (var themePath in Directory.GetDirectories(themesDirectory))
            {
                var themeJsonPath = Path.Combine(themePath, "theme.json");
                if (!File.Exists(themeJsonPath)) continue;

                var themeName = themePath.Substring(themePath.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                var jsonString = File.ReadAllText(themeJsonPath);
                var theme = JsonSerializer.Deserialize<Theme>(jsonString);
                theme.Name = themeName;
                theme.Path = themePath;
                _themes.Add(themeName, theme);
            }
        }

        public Theme Get(string name)
        {
            return _themes.FirstOrDefault(t => t.Key == name).Value;
        }

        public IEnumerable<string> GetThemesNames()
        {
            return _themes.Keys;
        }
    }
    public interface IThemesRepository
    {
        IEnumerable<string> GetThemesNames();
        Theme Get(string name);
    }
}
