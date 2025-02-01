using SFML.Graphics;
using SFML.System;
using Source.Tools;

namespace Agario
{
    public class Enemy : GameEntity
    {
        private static Random _random = new Random();
        private float _speed;

        public Enemy(Vector2f position, float speed, float growthFactor) : base(growthFactor)
        {
            _speed = speed;
            Shape = new CircleShape(45) { FillColor = Color.Red, Position = position };
            Shape.Origin = new Vector2f(45, 45);
            _direction = new Vector2f((float)(_random.NextDouble() * 2 - 1), (float)(_random.NextDouble() * 2 - 1)).Normalize();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Shape.Position += _direction * _speed * deltaTime;
        }

        public void Interact(List<Enemy> enemies, List<Food> foods, Player player, float deltaTime)
        {
            Vector2f directionToPlayer = (player.Shape.Position - Shape.Position).Normalize();

            Shape.Position += directionToPlayer * _speed * deltaTime;

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (this != enemies[i] && CheckCollision(enemies[i].Shape))
                {
                    if (Shape.Radius > enemies[i].Shape.Radius)
                    {
                        Grow();
                        enemies.RemoveAt(i);
                    }
                }
            }

            if (CheckCollision(player.Shape))
            {
                if (Shape.Radius > player.Shape.Radius)
                {
                    Grow();
                    player.Reset();
                }
            }

            for (int i = foods.Count - 1; i >= 0; i--)
            {
                if (CheckCollision(foods[i].Shape))
                {
                    Grow();
                    foods.RemoveAt(i);
                }
            }
        }
    }
}