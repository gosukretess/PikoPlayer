using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PikoPlayer.Config
{
    public static class ConfigurationSaveSystem
    {
        public static void Change(string key, object value)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "config.json");
            var configJson = File.ReadAllText(filePath);
            var config = JsonSerializer.Deserialize<Dictionary<string, object>>(configJson);
            config[key] = value;
            var updatedConfigJson = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, updatedConfigJson);
        }
    }
}
