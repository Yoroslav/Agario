using SFML.System;

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

        public static Vector2f Normalize(this Vector2f source)
        {
            float length = (float)Math.Sqrt(source.X * source.X + source.Y * source.Y);
            return length > 0 ? source / length : new Vector2f(0, 0);
        }

        public static float DistanceSquared(this Vector2f a, Vector2f b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return dx * dx + dy * dy;
        }
    }
}