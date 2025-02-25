using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using Agario.Entities;
using Source.Tools; 

namespace Agario.Entities
{
    public class Enemy : GameEntity
    {
        private static Random _random = new Random();
        private float _speed;
        private float _aggression;
        public Animator Animator { get; private set; }

        public bool MarkedToKill { get; set; } = false;

        public Enemy(Vector2f position, float speed, float growthFactor, float aggression, Animator animator) : base(growthFactor)
        {
            _speed = speed;
            _aggression = aggression;
            Animator = animator;
            Shape = new CircleShape(90) { Position = position };
            Shape.Origin = new Vector2f(45, 45);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Animator.IsMoving = true;
            Animator.Update(deltaTime, Shape.Position);
            Animator.SetScale(Shape.Radius / 15f, Shape.Radius / 15f);
        }

        public void Draw(RenderWindow window) => Animator.Draw(window);

        public void Interact(List<Enemy> enemies, List<Food> foods, Player player, float deltaTime)
        {
            Vector2f directionToPlayer = (player.Shape.Position - Shape.Position).Normalize(); 
            Shape.Position += directionToPlayer * _speed * deltaTime * _aggression;

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i].MarkedToKill) continue;

                if (this != enemies[i] && CheckCollision(enemies[i].Shape))
                {
                    if (Shape.Radius > enemies[i].Shape.Radius)
                    {
                        Grow();
                        enemies[i].MarkedToKill = true;
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
