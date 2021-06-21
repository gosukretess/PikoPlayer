using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace PikoPlayer.Config
{
    public static class ConfigurationExtensions
    {
        public static void Change(this IConfiguration configuration , string key, string value)
        {
            var configJson = File.ReadAllText("config.json");
            var config = JsonSerializer.Deserialize<Dictionary<string, object>>(configJson);
            config[key] = value;
            var updatedConfigJson = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("config.json", updatedConfigJson);
        }
    }
}
