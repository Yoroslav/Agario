using SFML.System;

namespace Source.Tools
{
    public static class Vector2fExtensions
    {
        public static float DistanceSquared(this Vector2f source, Vector2f second)
        {
            float deltaX = second.X - source.X;
            float deltaY = second.Y - source.Y;
            return deltaX * deltaX + deltaY * deltaY;  
        }

        public static Vector2f Normalize(this Vector2f source)
        {
            float magnitude = Magnitude(source);
            if (magnitude == 0) return new Vector2f(0, 0);
            return source / magnitude;
        }

        private static float Magnitude(Vector2f vector)
        {
            return (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }
    }
}