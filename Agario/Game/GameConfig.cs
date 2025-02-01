using SFML.Graphics;

namespace Agario
{
    public class GameConfig
    {
        public int ScreenWidth { get; set; } = 1600;
        public int ScreenHeight { get; set; } = 1200;
        public int InitialFoodCount { get; set; } = 100;
        public float FoodSpawnRate { get; set; } = 1.0f;
        public float PlayerSpeed { get; set; } = 200.0f;
        public float EnemySpeed { get; set; } = 100.0f;
        public float PlayerGrowthFactor { get; set; } = 2.0f;
        public float EnemyGrowthFactor { get; set; } = 2.0f;
        public Color BackgroundColor { get; set; } = new Color(255, 255, 255, 255);
    }
}