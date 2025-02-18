using Agario.Project.Game.Animations;
using SFML.Graphics;
using SFML.System;

namespace Agario.Entities
{
    public class Food
    {
        public CircleShape Shape { get; private set; }
        public Animator Animator { get; private set; }

        public Food(Vector2f position, Animator animator)
        {
            Shape = new CircleShape(10) { FillColor = Color.Green, Position = position };
            Shape.Origin = new Vector2f(10, 10);
            Animator = animator;
        }
    }
}
