namespace Agario.Project.Game.Configs
{
    internal class AudioConfig
    {
        static AudioConfig()
        {
            ConfigLoader.LoadConfig("AudioConfig");
        }

        public static string StartSound = "game_start";
        public static string EatFoodSound = "eat_food";
        public static string EatEnemySound = "eat_enemy";
        public static string PlayerDefeated = "player_defeated";
    }
}