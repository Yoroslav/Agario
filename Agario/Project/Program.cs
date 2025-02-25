using Agario;
using Agario.Project.Game.Configs;
using Agario.Project.Game.MenuSkins;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

public static class Program
{
    public static void Main()
    {
        using RenderWindow window = new RenderWindow(new VideoMode(1024, 768), "Agario Game");
        window.SetFramerateLimit(60);

        SoundSystem soundSystem = new SoundSystem("Assets/Sounds");
        SkinMenu skinMenu = new SkinMenu(window);

        skinMenu.OnPlay += () => StartGame(window, soundSystem, skinMenu.SelectedSkin);
        skinMenu.Run();
    }

    private static void StartGame(RenderWindow window, SoundSystem soundSystem, Texture playerSkin)
    {
        if (playerSkin == null)
            playerSkin = ResourceManagerXXXXX.GetDefaultSkin();

        var config = new GameConfig
        {
            ScreenWidth = 1024,
            ScreenHeight = 768,
            PlayerSpeed = 200f,
            PlayerGrowthFactor = 1.1f,
            InitialFoodCount = 50,
            MaxEnemies = 10,
            EnemySpeed = 150f,
            EnemyGrowthFactor = 1.05f,
            EnemyAggression = 1.0f,
            BackgroundColor = new Color(50, 50, 50)
        };

        GameScene gameScene = new GameScene(soundSystem);
        gameScene.Initialize(config);

        gameScene.GetPlayer().SetTexture(playerSkin);
        InputHandler inputHandler = new InputHandler(gameScene.GetPlayer(), gameScene.GetEnemies());

        Clock clock = new Clock();
        while (window.IsOpen)
        {
            window.DispatchEvents();
            float deltaTime = clock.Restart().AsSeconds();
            inputHandler.HandleInput();
            gameScene.Update(deltaTime);

            window.Clear(config.BackgroundColor);
            gameScene.Render(window);
            window.Display();
        }

        gameScene.Dispose();
    }
}