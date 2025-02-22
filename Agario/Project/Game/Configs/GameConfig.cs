using SFML.Graphics;

namespace Agario.Project.Game.Configs
{
    public class GameConfig
    {
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public float PlayerSpeed { get; set; }
        public float PlayerGrowthFactor { get; set; }
        public float EnemySpeed { get; set; }
        public float EnemyGrowthFactor { get; set; }
        public float EnemyAggression { get; set; }
        public int InitialFoodCount { get; set; }
        public int MaxEnemies { get; set; }
        public SFML.Graphics.Color BackgroundColor { get; set; }
    }
}