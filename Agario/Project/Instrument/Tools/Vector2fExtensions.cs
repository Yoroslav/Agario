using SFML.System;
using System;

namespace Source.Tools
{
    public static class VectorExtensions
    {
        public static float DistanceTo(this Vector2f a, Vector2f b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public static Vector2f Normalize(this Vector2f vector)
        {
            float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (length == 0)
                return new Vector2f(0, 0);
            return new Vector2f(vector.X / length, vector.Y / length);
        }

        public static float DistanceSquared(this Vector2f a, Vector2f b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return dx * dx + dy * dy;
        }
    }
}
