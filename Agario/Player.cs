using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Source.Tools;

namespace Agario
{
    public class Player
    {
        public CircleShape Shape { get; private set; }
        private float _speed = 200f;
        private Vector2f _direction;
        public int Score { get; private set; } = 0;

        public Player(Vector2f position)
        {
            Shape = new CircleShape(20)
            {
                FillColor = Color.Blue,
                Position = position
            };
            Shape.Origin = new Vector2f(20, 20);
        }

        public void HandleInput()
        {
            var direction = new Vector2f();

            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) direction.Y -= 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) direction.Y += 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) direction.X -= 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) direction.X += 1;

            _direction = direction.Normalize();
        }
        public void SwapWithClosestEnemy(List<Enemy> enemies)
        {
            if (enemies.Count == 0) return;

            Enemy closestEnemy = null;
            float minDistance = float.MaxValue;

            foreach (var enemy in enemies)
            {
                Vector2f diff = Shape.Position - enemy.Shape.Position;
                float distance = MathF.Sqrt(diff.X * diff.X + diff.Y * diff.Y);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }

            if (closestEnemy != null)
            {
                Vector2f tempPosition = Shape.Position;
                float tempRadius = Shape.Radius;

                Shape.Position = closestEnemy.Shape.Position;
                Shape.Radius = closestEnemy.Shape.Radius;
                Shape.Origin = new Vector2f(Shape.Radius, Shape.Radius);

                closestEnemy.Shape.Position = tempPosition;
                closestEnemy.Shape.Radius = tempRadius;
                closestEnemy.Shape.Origin = new Vector2f(tempRadius, tempRadius);
            }
        }


        public void Update(float deltaTime)
        {
            Shape.Position += _direction * _speed * deltaTime;

            Shape.Position = new Vector2f(
                Math.Clamp(Shape.Position.X, Shape.Radius, 1600 - Shape.Radius),
                Math.Clamp(Shape.Position.Y, Shape.Radius, 1200 - Shape.Radius)
            );
        }

        public bool CheckCollision(CircleShape other)
        {
            return Shape.Position.IsColliding(Shape.Radius, other.Position, other.Radius);
        }

        public bool IsLargerThan(Enemy enemy)
        {
            return Shape.Radius > enemy.Shape.Radius;
        }

        public void Grow()
        {
            Shape.Radius += 2;
            Shape.Origin = new Vector2f(Shape.Radius, Shape.Radius);
            Score += 10;
        }

        public void MarkAsDefeated()
        {
            Reset();
        }

        public void Reset()
        {
            Shape.Position = new Vector2f(800, 600); 
            Shape.Radius = 20;
            Shape.Origin = new Vector2f(20, 20);
            Score = 0;
        }
    }
}
