using Engine;
using Game;
using SFML.System;
using SFML.Graphics;

class Program
{
    public static float deltaTime; 

    static void Main(string[] args)
    {
        var window = new GameWindow(1600, 1200, "Agar.io Clone");
        var gameScene = new GameScene();

        var clock = new Clock();

        while (window.IsOpen)
        {
            deltaTime = clock.Restart().AsSeconds();

            window.DispatchEvents();
            gameScene.Input();
            gameScene.Update(deltaTime);

            window.Clear(Color.Black);
            gameScene.Render(window);
            window.Display();
        }
    }
}