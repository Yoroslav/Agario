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
        private IGameRules _gameRules;
        private GameConfig _config;

        public GameLoop(GameConfig config, IGameRules gameRules)
        {
            _config = config;
            _gameRules = gameRules;

            _window = new RenderWindow(new VideoMode((uint)_config.ScreenWidth, (uint)_config.ScreenHeight), "Agar.io Clone");
            _window.Closed += (sender, e) => _window.Close();

            _clock = new Clock();
            _gameRules.Initialize(config);
        }

        public void Run()
        {
            while (_window.IsOpen)
            {
                float deltaTime = _clock.Restart().AsSeconds();

                _window.DispatchEvents();
                _gameRules.HandleInput();
                _gameRules.Update(deltaTime);

                _window.Clear(_config.BackgroundColor);
                _gameRules.Render(_window);
                _window.Display();
            }
        }
    }
}
