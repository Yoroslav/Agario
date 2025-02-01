using Agario;
using Engine;

class Program
{
    static void Main(string[] args)
    {
        var config = new GameConfig
        {
            ScreenWidth = 1600,
            ScreenHeight = 1200,
            InitialFoodCount = 100,
            FoodSpawnRate = 1.0f,
            PlayerSpeed = 200.0f,
            EnemySpeed = 100.0f,
            PlayerGrowthFactor = 2.0f,
            EnemyGrowthFactor = 2.0f,
            BackgroundColor = new Color(255, 255, 255, 255)
        };

        GameLoop gameLoop = new GameLoop(config);
        gameLoop.Run();
    }
}