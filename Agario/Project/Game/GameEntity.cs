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
        public bool markedToKill { get; set; }

        public GameEntity(float growthFactor)
        {
            _growthFactor = growthFactor;
        }

        public virtual void Update(float deltaTime)
        {
            Shape.Position += _direction * deltaTime;
            HandleWallCollision();
        }

        public void Grow()
        {
            if (Shape is CircleShape circle)
            {
                circle.Radius *= _growthFactor;
                circle.Origin = new Vector2f(circle.Radius, circle.Radius);
            }
        }

        public bool CheckCollision(CircleShape other)
        {
            return Shape.GetGlobalBounds().Intersects(other.GetGlobalBounds()) &&
                   Shape.Position.DistanceTo(other.Position) < Shape.Radius + other.Radius;
        }

        protected void HandleWallCollision()
        {
            const float screenWidth = 1600;
            const float screenHeight = 1200;

            Shape.Position = new Vector2f(
                HandleAxisCollision(Shape.Position.X, screenWidth, ref _direction.X),
                HandleAxisCollision(Shape.Position.Y, screenHeight, ref _direction.Y)
            );
        }

        private float HandleAxisCollision(float position, float screenSize, ref float axisDirection)
        {
            if (Shape.Radius > screenSize / 2)
            {
                axisDirection = 0;
                return screenSize / 2;
            }
            else
            {
                if (position < Shape.Radius || position > screenSize - Shape.Radius)
                {
                    axisDirection *= -1;
                }
                return Math.Clamp(position, Shape.Radius, screenSize - Shape.Radius);
            }
        }

    }
}