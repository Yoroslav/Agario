using Agario;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Engine
{
    public class GameLoop
    {
        private RenderWindow _window;
        private Clock _clock;
        private GameScene _gameScene;
        private GameConfig _config;

        public GameLoop(GameConfig config)
        {
            _config = config;
            _window = new RenderWindow(new VideoMode((uint)_config.ScreenWidth, (uint)_config.ScreenHeight), "Agar.io Clone");
            _clock = new Clock();
            _gameScene = new GameScene(_config);
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

                _window.Clear(_config.BackgroundColor);
                _gameScene.Render(_window);
                _window.Display();
            }
        }
    }
}