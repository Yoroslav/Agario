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
        private bool _isEnabled = true;

        public UIButton(Texture texture, Font font, string label, Vector2f position, Action action, Vector2f? scale = null)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture), "Texture cannot be null.");
            if (font == null)
                throw new ArgumentNullException(nameof(font), "Font cannot be null.");

            _sprite = new Sprite(texture)
            {
                Position = position,
                Scale = scale ?? new Vector2f(1, 1)
            };

            _text = new Text(label, font, 28)
            {
                Position = position + new Vector2f(20, 10),
                FillColor = Color.Black
            };

            _action = action;
        }

        public void SetEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
            _sprite.Color = isEnabled ? Color.White : new Color(150, 150, 150);
        }

        public void UpdateDraw(RenderWindow window)
        {
            if (_sprite == null || _text == null || window == null)
                return;

            var mousePos = Mouse.GetPosition(window);
            var bounds = _sprite.GetGlobalBounds();
            bool isHovered = bounds.Contains(mousePos.X, mousePos.Y);

            if (_isEnabled)
            {
                _sprite.Color = isHovered ? new Color(200, 200, 200) : Color.White;

                if (isHovered && Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    _action?.Invoke();
                }
            }

            window.Draw(_sprite);
            window.Draw(_text);
        }
    }
}