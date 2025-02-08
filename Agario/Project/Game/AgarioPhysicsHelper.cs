using SFML.Graphics;
using SFML.System;
using Source.Tools; 

namespace Agario
{
    public static class AgarioPhysicsHelper
    {
        public static bool IsColliding(this CircleShape aShape, CircleShape bShape)
        {
            float sumOfRadius = aShape.Radius + bShape.Radius;
            return aShape.Position.DistanceSquared(bShape.Position) < sumOfRadius * sumOfRadius;
        }
    }
}
