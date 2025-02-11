using SFML.Graphics;

namespace Agario
{
    public class GameConfig
    {
        public int ScreenWidth { get; set; } = 1000;
        public int ScreenHeight { get; set; } = 1000;
        public int InitialFoodCount { get; set; } = 10;
        public float FoodSpawnRate { get; set; } = 11.0f;
        public float PlayerSpeed { get; set; } = 200.0f;
        public float EnemySpeed { get; set; } = 100.0f;
        public float PlayerGrowthFactor { get; set; } = 2.0f;
        public float EnemyGrowthFactor { get; set; } = 2.0f;
        public Color BackgroundColor { get; set; } = new Color(130, 110, 30,230);
        public int MaxEnemies { get; set; } = 1;
        public float EnemySpawnRate { get; set; } = 5.0f;
        public float PlayerInitialRadius { get; set; } = 0.0f;
        public float FoodRadius { get; set; } = 100.0f;
        public float EnemyInitialRadius { get; set; } = 210.0f;

        public float EnemyAggression { get; set; } = 2.0f;
    }
}