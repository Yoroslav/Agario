using SFML.Graphics;
using SFML.System;
using System.IO;

namespace Agario
{
    public static class UnitFactory
    {
        private static Texture LoadTextureFromResource(byte[] imageData)
        {
            using (var stream = new MemoryStream(imageData))
            {
                return new Texture(stream);
            }
        }
        public static Player CreatePlayer(Vector2f position, float speed, float growthFactor, Texture skinTexture, float screenWidth, float screenHeight)
        {
            var animator = new Animator(skinTexture, 32, 64, 4, 0.15f);
            animator.SetScale(1f, 1f);
            return new Player(position, speed, growthFactor, animator, screenWidth, screenHeight);
        }
    }
}
