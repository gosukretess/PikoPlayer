using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PikoPlayer.Config
{
    public static class ConfigurationSaveSystem
    {
        public static void Change(string key, object value)
        {
            var configJson = File.ReadAllText("config.json");
            var config = JsonSerializer.Deserialize<Dictionary<string, object>>(configJson);
            config[key] = value;
            var updatedConfigJson = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("config.json", updatedConfigJson);
        }
    }
}
