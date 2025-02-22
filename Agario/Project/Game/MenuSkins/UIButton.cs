using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Agario.Project.Game.MenuSkins
{
    public class UIButton
    {
        private readonly Sprite _sprite;
        private readonly Text _text;
        private readonly Action _action;

        public UIButton(Texture texture, Font font, string label, Vector2f position, Action action, Vector2f? scale = null)
        {
            _sprite = new Sprite(texture)
            {
                Position = position,
                Scale = scale ?? new Vector2f(1, 1)
            };

            _text = new Text(label, font, 20)
            {
                Position = position + new Vector2f(20, 10),
                FillColor = Color.Black
            };

            _action = action;
        }

        public void UpdateDraw(RenderWindow window)
        {
            var mousePos = Mouse.GetPosition(window);
            var bounds = _sprite.GetGlobalBounds();

            _sprite.Color = bounds.Contains(mousePos.X, mousePos.Y)
                ? new Color(200, 200, 200)
                : Color.White;

            if (bounds.Contains(mousePos.X, mousePos.Y))
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    _action?.Invoke();

            window.Draw(_sprite);
            window.Draw(_text);
        }
    }
}