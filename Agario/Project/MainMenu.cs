using Agario.Project.Game.MenuSkins;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Agario.Project
{
    public class MainMenu
    {
        private RenderWindow _window;
        private List<UIButton> _buttons = new();
        private Font _font;

        public MainMenu(RenderWindow window, Action playAction, Action exitAction)
        {
            _window = window;
            _font = ResourceManager.GetFont();

            var playButton = new UIButton(
                ResourceManager.GetUITexture("button_base"),
                _font,
                "Play",
                new Vector2f(500, 300),
                playAction
            );

            var exitButton = new UIButton(
                ResourceManager.GetUITexture("button_base"),
                _font,
                "Exit",
                new Vector2f(500, 400),
                exitAction
            );

            _buttons.Add(playButton);
            _buttons.Add(exitButton);
        }

        public void Update(float deltaTime)
        {
            foreach (var btn in _buttons)
                btn.UpdateDraw(_window);
        }

        public void Draw()
        {
            _window.Clear(new Color(40, 40, 40));
            foreach (var btn in _buttons)
                btn.UpdateDraw(_window);
            _window.Display();
        }
    }
}
