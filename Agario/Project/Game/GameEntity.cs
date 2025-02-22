using SFML.Graphics;
using SFML.System;
using Source.Tools;

namespace Agario
{
    public abstract class GameEntity
    {
        public CircleShape Shape { get; protected set; }
        protected float _growthFactor;
        protected Vector2f _direction;
        public bool MarkedToKill { get; set; }

        public GameEntity(float growthFactor)
        {
            _growthFactor = growthFactor;
        }

        public virtual void Update(float deltaTime)
        {
            Shape.Position += _direction * deltaTime;
            HandleWallCollision();
        }

        public virtual void Grow()
        {
            Shape.Radius += _growthFactor;
            Shape.Origin = new Vector2f(Shape.Radius, Shape.Radius);
        }

        public bool CheckCollision(CircleShape other)
        {
            return Shape.GetGlobalBounds().Intersects(other.GetGlobalBounds()) &&
                   Shape.Position.DistanceTo(other.Position) < Shape.Radius + other.Radius;
        }

        protected void HandleWallCollision()
        {
            if (Shape.Position.X < Shape.Radius || Shape.Position.X > 1600 - Shape.Radius)
                _direction.X *= -1;

            if (Shape.Position.Y < Shape.Radius || Shape.Position.Y > 1200 - Shape.Radius)
                _direction.Y *= -1;

            Shape.Position = new Vector2f(
                Math.Clamp(Shape.Position.X, Shape.Radius, 1600 - Shape.Radius),
                Math.Clamp(Shape.Position.Y, Shape.Radius, 1200 - Shape.Radius)
            );
        }
    }
}