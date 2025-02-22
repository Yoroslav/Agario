using Agario;
using Agario.Project;
using Agario.Project.Game.Configs;
using Agario.Project.Game.MenuSkins;
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
        private GameState _currentState;
        private MainMenu _mainMenu;
        private SkinMenu _skinMenu;

        public enum GameState
        {
            MainMenu,
            SkinSelection,
            Playing,
            Paused
        }

        public GameLoop(GameConfig config, IGameRules gameRules)
        {
            _config = config;
            _gameRules = gameRules;

            _window = new RenderWindow(
                new VideoMode((uint)_config.ScreenWidth, (uint)_config.ScreenHeight),
                "Agar.io Clone"
            );

            _window.Closed += (sender, e) => _window.Close();
            _clock = new Clock();

            InitializeMenus();
            SwitchState(GameState.MainMenu);
        }

        private void InitializeMenus()
        {
            _mainMenu = new MainMenu(_window,
                () => SwitchState(GameState.SkinSelection), // Play
                () => _window.Close() // Exit
            );

            _skinMenu = new SkinMenu(_window);
            _skinMenu.OnPlay += () => SwitchState(GameState.Playing);
        }

        public void SwitchState(GameState newState)
        {
            _currentState = newState;

            switch (newState)
            {
                case GameState.Playing:
                    _gameRules.Initialize(_config);
                    break;
            }
        }

        public void Run()
        {
            while (_window.IsOpen)
            {
                float deltaTime = _clock.Restart().AsSeconds();
                _window.DispatchEvents();

                switch (_currentState)
                {
                    case GameState.MainMenu:
                        _mainMenu.Update(deltaTime);
                        _mainMenu.Draw();
                        break;

                    case GameState.SkinSelection:
                        _skinMenu.Run();
                        break;

                    case GameState.Playing:
                        UpdateGame(deltaTime);
                        break;
                }
            }
        }

        private void UpdateGame(float deltaTime)
        {
            _gameRules.HandleInput();
            _gameRules.Update(deltaTime);

            _window.Clear(_config.BackgroundColor);
            _gameRules.Render(_window);
            _window.Display();
        }
    }
}