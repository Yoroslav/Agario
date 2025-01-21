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
            return magnitude > 0 ? source / magnitude : new Vector2f(0, 0);
        }

        public static bool IsColliding(this Vector2f source, float radius1, Vector2f target, float radius2)
        {
            float combinedRadius = radius1 + radius2;
            return source.DistanceSquared(target) <= (combinedRadius * combinedRadius);
        }

        private static float Magnitude(Vector2f vector)
        {
            return (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }
    }
}