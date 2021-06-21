﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace PikoPlayer.Themes
{
    public class ThemesRepository : IThemesRepository
    {
        private readonly IDictionary<string, Theme> _themes = new Dictionary<string, Theme>();
        public Theme ActiveTheme { get; set; }

        public ThemesRepository()
        {
            var themesDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources", "Themes");
            foreach (var themePath in Directory.GetDirectories(themesDirectory))
            {
                var themeName = themePath.Substring(themePath.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                var jsonString = File.ReadAllText(Path.Combine(themePath, "theme.json"));
                var theme = JsonSerializer.Deserialize<Theme>(jsonString);
                theme.Name = themeName;
                theme.Path = themePath;
                _themes.Add(themeName, theme);
            }
        }

        public Theme GetThemeByName(string name)
        {
            return _themes.FirstOrDefault(t => t.Key == name).Value;
        }

        public IEnumerable<string> GetThemesList()
        {
            return _themes.Keys;
        }
    }
    public interface IThemesRepository
    {
        IEnumerable<string> GetThemesList();
        Theme GetThemeByName(string name);
    }
}
