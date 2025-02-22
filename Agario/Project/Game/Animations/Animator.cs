using SFML.Graphics;
using SFML.System;

public class Animator
{
    private readonly Texture _texture;
    private readonly Sprite _sprite;
    private readonly int _frameWidth;
    private readonly int _frameHeight;
    private readonly int _totalFrames;
    private readonly float _updateInterval;
    private float _timer;
    private int _currentFrame;
    public bool IsMoving { get; set; }
    public int CurrentRow { get; private set; }

    public Animator(Texture texture, int frameWidth, int frameHeight, int totalFrames, float updateInterval)
    {
        _texture = texture;
        _frameWidth = frameWidth;
        _frameHeight = frameHeight;
        _totalFrames = totalFrames;
        _updateInterval = updateInterval;
        _sprite = new Sprite(_texture);
    }

    public void Update(float deltaTime, Vector2f position)
    {
        if (!IsMoving) return;

        _timer += deltaTime;
        if (_timer >= _updateInterval)
        {
            _currentFrame = (_currentFrame + 1) % _totalFrames;
            _timer = 0;
        }

        _sprite.TextureRect = new IntRect(
            _currentFrame * _frameWidth,
            CurrentRow * _frameHeight,
            _frameWidth,
            _frameHeight
        );

        _sprite.Position = position;
    }

    public void Draw(RenderWindow window) => window.Draw(_sprite);
    public void SetScale(float x, float y) => _sprite.Scale = new Vector2f(x, y);
    public void SetRow(int row) => CurrentRow = row;
}