using SFML.Graphics;
using SFML.System;

namespace Agario
{
    public class Food
    {
        public CircleShape Shape { get; private set; }

        public Food(Vector2f position)
        {
            Shape = new CircleShape(5) { FillColor = Color.Green, Position = position };
        }
    }
}