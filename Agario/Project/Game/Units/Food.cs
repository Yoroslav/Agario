using SFML.Graphics;
using SFML.System;
using Agario.Project.Game.Animations;

namespace Agario.Entities
{
    public class Food : GameEntity
    {
        public Animator Animator { get; private set; }
        public Food(Vector2f position, Animator animator) : base(1.0f)
        {
            Shape = new CircleShape(10)
            {
                Position = position,
                FillColor = Color.Green
            };
            Shape.Origin = new Vector2f(10, 10);
            Animator = animator;
        }
        public override void Update(float deltaTime)
        {
            Animator.IsMoving = true;
            Animator.Update(deltaTime, Shape.Position);
        }
        public void Draw(RenderWindow window) => Animator.Draw(window);
    }
}
