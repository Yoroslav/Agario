using SFML.Graphics;
using SFML.System;
using System;

namespace Agario
{
    public class Enemy
    {
        public CircleShape Shape { get; private set; }
        private static Random _random = new Random();
        private float _growthFactor;
        private Vector2f _direction;

        public Enemy(Vector2f position)
        {
            _growthFactor = _random.Next(50, 200);
            Shape = new CircleShape(45) { FillColor = Color.Red, Position = position };

            _direction = new Vector2f((float)(_random.NextDouble() * 2 - 1), (float)(_random.NextDouble() * 2 - 1));
            _direction = Normalize(_direction);
        }

        public void Update()
        {
            Shape.Position += _direction * 100 * Program.deltaTime;
            if (Shape.Position.X < 0 || Shape.Position.X > 1600 - Shape.Radius * 2)
                _direction.X *= -1;

            if (Shape.Position.Y < 0 || Shape.Position.Y > 1200 - Shape.Radius * 2)
                _direction.Y *= -1;

            Shape.Position = new Vector2f(
                Math.Clamp(Shape.Position.X, 0, 1600 - Shape.Radius * 2),
                Math.Clamp(Shape.Position.Y, 0, 1200 - Shape.Radius * 2)
            );
        }

        public void Grow()
        {
            Shape.Radius += _growthFactor;
            Shape.Origin = new Vector2f(Shape.Radius, Shape.Radius);
        }

        private Vector2f Normalize(Vector2f vector)
        {
            float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            return length > 0 ? vector / length : vector;
        }
    }
}