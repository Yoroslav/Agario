using Game;
using SFML.Graphics;
using SFML.System;
using Source.Tools;
using System;
using System.Collections.Generic;

namespace Agario
{
    public class Enemy
    {
        public CircleShape Shape { get; private set; }
        private static Random _random = new Random();
        private float _growthFactor = 2.0f;
        private Vector2f _direction;

        public Enemy(Vector2f position)
        {
            Shape = new CircleShape(45) { FillColor = Color.Red, Position = position };
            _direction = new Vector2f((float)(_random.NextDouble() * 2 - 1), (float)(_random.NextDouble() * 2 - 1));
            _direction = Normalize(_direction);
        }

        public void Update(List<Enemy> enemies, Player player)
        {
            Vector2f directionToPlayer = player.Shape.Position - Shape.Position;
            directionToPlayer = Normalize(directionToPlayer);

            Shape.Position += directionToPlayer * 100 * GameScene.deltaTime; 

            AbstainingWalls(); 

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (this != enemies[i] && CheckCollision(enemies[i]))
                {
                    if (Shape.Radius > enemies[i].Shape.Radius)
                    {
                        Grow();
                        enemies.RemoveAt(i);
                    }
                }
            }

            if (CheckCollisionWithPlayer(player))
            {
                if (Shape.Radius > player.Shape.Radius)
                {
                    Grow();
                    player.Reset();
                }
            }
        }

        private bool CheckCollisionWithPlayer(Player player)
        {
            float combinedRadius = Shape.Radius + player.Shape.Radius;
            return Shape.Position.DistanceSquared(player.Shape.Position) <= (combinedRadius * combinedRadius);
        }

        private void AbstainingWalls()
        {
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

        private bool CheckCollision(Enemy enemy)
        {
            float combinedRadius = Shape.Radius + enemy.Shape.Radius;
            return Shape.Position.DistanceSquared(enemy.Shape.Position) <= (combinedRadius * combinedRadius);
        }

        private Vector2f Normalize(Vector2f vector)
        {
            float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            return length > 0 ? vector / length : vector;
        }
    }
}