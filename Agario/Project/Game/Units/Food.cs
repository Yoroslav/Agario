using SFML.Graphics;
using SFML.System;

namespace Agario.Entities
{
    public class Food
    {
        public CircleShape Shape { get; private set; }

        public Food(Vector2f position)
        {
            Shape = new CircleShape(5);
            Shape.Position = position;
            Shape.FillColor = Color.Green;
        }
    }
}