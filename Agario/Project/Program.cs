using Agario.Project.Game;
using Agario.Project.Game.Configs;
using SFML.Graphics;
using SFML.Window;
using System;
using SFML.System;
using Agario;

class Program
{
    static void Main()
    {
        using (RenderWindow window = new RenderWindow(new VideoMode(1600, 1200), "Skin Selector"))
        {
            var menu = new SkinMenu(window);
            menu.Run();

            if (menu.SelectedSkin != null)
            {
                var gameConfig = LoadGameConfig();
                var soundSystem = new SoundSystem("Assets/Sounds");
                var gameScene = new GameScene(soundSystem, menu.SelectedSkin);
                gameScene.Initialize(gameConfig);
                RunGame(gameScene);
            }
        }
    }

    private static GameConfig LoadGameConfig()
    {
        return new GameConfig
        {
            ScreenWidth = 1600,
            ScreenHeight = 1200,
            PlayerSpeed = 200f,
            PlayerGrowthFactor = 5f,
            EnemySpeed = 150f,
            EnemyGrowthFactor = 3f,
            EnemyAggression = 1.2f,
            InitialFoodCount = 50,
            MaxEnemies = 10,
            BackgroundColor = Color.Cyan
        };
    }

    private static void RunGame(GameScene gameScene)
    {
        using RenderWindow window = new RenderWindow(new VideoMode(1600, 1200), "Agar.io");
        window.SetVerticalSyncEnabled(true);
        window.Closed += (sender, e) => window.Close();

        Clock clock = new Clock();
        while (window.IsOpen)
        {
            window.DispatchEvents();
            float deltaTime = clock.Restart().AsSeconds();

            gameScene.HandleInput();
            gameScene.Update(deltaTime);
            gameScene.Render(window);

            window.Display();
        }
    }
}