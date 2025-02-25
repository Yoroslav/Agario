using Agario.Entities;
using SFML.Graphics;
using SFML.System;
using Source.Tools;
using System;
using System.Collections.Generic;

namespace Agario
{
    public class Player
    {
        public CircleShape Shape { get; private set; }
        public Animator Animator { get; set; }
        private float _speed;
        private Vector2f _direction;
        private float _growthFactor;
        private float _screenWidth;
        private float _screenHeight;
        private float _maxRadius;
        public int Score { get; private set; } = 0;
        private Sprite _sprite; 

        public Player(Vector2f position, float speed, float growthFactor, Animator animator, float screenWidth, float screenHeight)
        {
            _speed = speed;
            _growthFactor = growthFactor;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _maxRadius = Math.Min(screenWidth, screenHeight) / 2 * 0.9f;
            Animator = animator;

            Shape = new CircleShape(20)
            {
                FillColor = Color.Blue,
                Position = position,
                Origin = new Vector2f(20, 20)
            };

            _sprite = new Sprite();
        }
        public void SetTexture(Texture texture)
        {
            if (_sprite != null)
            {
                _sprite.Texture = texture; 
                _sprite.Origin = new Vector2f(texture.Size.X / 2, texture.Size.Y / 2); 
            }
        }
        public void Update(float deltaTime)
        {
            if (_direction != new Vector2f(0, 0))
                Shape.Position += _direction * _speed * deltaTime;

            float clampXMin = Shape.Radius;
            float clampXMax = _screenWidth - Shape.Radius;
            float clampYMin = Shape.Radius;
            float clampYMax = _screenHeight - Shape.Radius;

            if (clampXMin > clampXMax)
                clampXMin = clampXMax = _screenWidth / 2;
            if (clampYMin > clampYMax)
                clampYMin = clampYMax = _screenHeight / 2;

            Shape.Position = new Vector2f(
                Math.Clamp(Shape.Position.X, clampXMin, clampXMax),
                Math.Clamp(Shape.Position.Y, clampYMin, clampYMax)
            );

            if (Animator != null)
            {
                Animator.IsMoving = (_direction != new Vector2f(0, 0));
                Animator.Update(deltaTime, Shape.Position);
            }
        }
        public void SwapWithClosestEnemy(List<Enemy> enemies)
        {
            if (enemies.Count == 0) return;

            Enemy closestEnemy = null;
            float minDistance = float.MaxValue;

            foreach (var enemy in enemies)
            {
                Vector2f diff = Shape.Position - enemy.Shape.Position;
                float distance = (float)Math.Sqrt(diff.X * diff.X + diff.Y * diff.Y);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }

            if (closestEnemy != null)
            {
                (Shape.Position, closestEnemy.Shape.Position) =
                    (closestEnemy.Shape.Position, Shape.Position);
            }
        }

        public bool CheckCollision(CircleShape other)
        {
            Vector2f delta = Shape.Position - other.Position;
            float distance = (float)Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);
            return distance < (Shape.Radius + other.Radius);
        }

        public bool IsLargerThan(Enemy enemy) => Shape.Radius > enemy.Shape.Radius;

        public void Grow()
        {
            float newRadius = Shape.Radius + _growthFactor;

            if (newRadius > _maxRadius)
                newRadius = _maxRadius;

            Shape.Radius = newRadius;
            Shape.Origin = new Vector2f(Shape.Radius, Shape.Radius);
            Shape.Scale = new Vector2f(Shape.Radius / 20, Shape.Radius / 20);
            Score += 10;
        }

        public void MarkAsDefeated() => Reset();

        public void Reset()
        {
            Shape.Position = GetRandomPosition(_screenWidth, _screenHeight, 20);
            Shape.Radius = 20;
            Shape.Origin = new Vector2f(20, 20);
            Score = 0;
        }

        public static Vector2f GetRandomPosition(float screenWidth, float screenHeight, float radius)
        {
            Random random = new Random();
            float x = (float)random.NextDouble() * (screenWidth - 2 * radius) + radius;
            float y = (float)random.NextDouble() * (screenHeight - 2 * radius) + radius;
            return new Vector2f(x, y);
        }

        public void Move(Vector2f direction) => _direction = direction.Normalize();
    }
}