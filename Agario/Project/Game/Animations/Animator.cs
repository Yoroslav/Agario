using SFML.Graphics;
using SFML.System;

namespace Agario.Project.Game.Animations
{
    public class Animator
    {
        private Sprite _sprite;
        public Sprite Sprite => _sprite;

        private int _frameWidth;
        private int _frameHeight;
        private int _totalFrames;
        private float _updateInterval;

        private float _time;
        private int _currentFrame;
        private int _row;

        public bool IsMoving { get; set; }

        public Animator(Texture texture, int frameWidth, int frameHeight, int totalFrames, float updateInterval)
        {
            _sprite = new Sprite(texture);
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _totalFrames = totalFrames;
            _updateInterval = updateInterval;

            _row = 0;
            _sprite.Origin = new Vector2f(frameWidth / 2f, frameHeight / 2f);
        }

        public void Update(float deltaTime, Vector2f position)
        {
            if (IsMoving)
            {
                _time += deltaTime;
                if (_time >= _updateInterval)
                {
                    _currentFrame = (_currentFrame + 1) % _totalFrames;
                    _time = 0f;
                }
            }
            else
            {
                _currentFrame = 0;
            }

            var rect = new IntRect(
                _currentFrame * _frameWidth,
                _row * _frameHeight,
                _frameWidth,
                _frameHeight
            );
            _sprite.TextureRect = rect;

            _sprite.Position = position;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(_sprite, states);
        }

        public void SetScale(float scaleX, float scaleY)
        {
            _sprite.Scale = new Vector2f(scaleX, scaleY);
        }

        public void SetRow(int row)
        {
            _row = row;
        }
    }
}