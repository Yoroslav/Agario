﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Source.Tools;
using Agario.Project.Game.Animations;

namespace Agario
{
    public class Player
    {
        public CircleShape Shape { get; private set; }
        public AnimationSystem Animation { get; private set; }
        private float _speed;
        private Vector2f _direction;
        public int Score { get; private set; } = 0;
        private float _growthFactor;

        public Player(Vector2f position, float speed, float growthFactor, Texture animationTexture)
        {
            _speed = speed;
            _growthFactor = growthFactor;
            Shape = new CircleShape(20)
            {
                FillColor = Color.Blue,
                Position = position
            };
            Shape.Origin = new Vector2f(20, 20);

            Animation = new AnimationSystem(animationTexture, 180, 180);
        }

        public void HandleInput()
        {
            _direction = new Vector2f();

            if (Keyboard.IsKeyPressed(Keyboard.Key.W)) _direction.Y -= 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S)) _direction.Y += 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A)) _direction.X -= 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D)) _direction.X += 1;

            if (_direction != new Vector2f(0, 0))
            {
                _direction = _direction.Normalize();
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
                float distance = MathF.Sqrt(diff.X * diff.X + diff.Y * diff.Y);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }

            if (closestEnemy != null)
            {
                (Shape.Position, Shape.Radius, Shape.Origin, closestEnemy.Shape.Position, closestEnemy.Shape.Radius, closestEnemy.Shape.Origin) =
                (closestEnemy.Shape.Position, closestEnemy.Shape.Radius, closestEnemy.Shape.Origin, Shape.Position, Shape.Radius, Shape.Origin);
            }
        }

        public void Update(float deltaTime)
        {
            HandleInput();
            if (_direction != new Vector2f(0, 0))
            {
                Shape.Position += _direction * _speed * deltaTime;
            }
            Shape.Position = new Vector2f(
                Math.Clamp(Shape.Position.X, Shape.Radius, 1600 - Shape.Radius),
                Math.Clamp(Shape.Position.Y, Shape.Radius, 1200 - Shape.Radius)
            );

            Animation.Update(deltaTime, Shape.Position);
        }

        public void Draw(RenderWindow window)
        {
            Animation.Draw(window, RenderStates.Default);
        }

        public bool CheckCollision(CircleShape other)
        {
            return Shape.IsColliding(other);
        }

        public bool IsLargerThan(Enemy enemy)
        {
            return Shape.Radius > enemy.Shape.Radius;
        }

        public void Grow()
        {
            Shape.Radius += _growthFactor;
            Shape.Origin = new Vector2f(Shape.Radius, Shape.Radius);
            Score += 10;
        }

        public void MarkAsDefeated()
        {
            Reset();
        }

        public void Reset()
        {
            Shape.Position = GetRandomPosition(800, 600, 20);
            Shape.Radius = 20;
            Shape.Origin = new Vector2f(20, 20);
            Score = 0;
        }

        public static Vector2f GetRandomPosition(float maxWidth, float maxHeight, float radius)
        {
            Random random = new Random();
            float x = (float)random.NextDouble() * (maxWidth - 2 * radius) + radius;
            float y = (float)random.NextDouble() * (maxHeight - 2 * radius) + radius;
            return new Vector2f(x, y);
        }

        public void Move(Vector2f direction)
        {
            _direction = direction.Normalize();
        }
    }
}