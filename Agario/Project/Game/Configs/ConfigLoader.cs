using Agario.Project.Game.Configs;
using System.IO;
using System.Text.Json;

namespace Agario
{
    public static class ConfigLoader
    {
        public static GameConfig LoadConfig(string filePath)
        {
            if (!File.Exists(filePath))
            {
                var defaultConfig = new GameConfig();
                SaveConfig(filePath, defaultConfig);
                return defaultConfig;
            }

            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<GameConfig>(json);
        }

        public static void SaveConfig(string filePath, GameConfig config)
        {
            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}