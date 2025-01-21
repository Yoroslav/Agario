using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Engine
{
    public class GameLoop
    {
        private RenderWindow _window;
        private Clock _clock;
        private GameScene _gameScene;

        public GameLoop()
        {
            _window = new RenderWindow(new VideoMode(1600, 1200), "Agar.io Clone");
            _clock = new Clock();
            _gameScene = new GameScene();
            _window.Closed += (sender, e) => _window.Close();
        }

        public void Run()
        {
            while (_window.IsOpen)
            {
                float deltaTime = _clock.Restart().AsSeconds();

                _window.DispatchEvents();

                _gameScene.HandleInput();
                _gameScene.Update(deltaTime);

                _window.Clear(Color.White);
                _gameScene.Render(_window);
                _window.Display();
            }
        }
    }
}