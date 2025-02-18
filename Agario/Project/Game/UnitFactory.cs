using Agario.Project.Game.Animations;
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

        public static Player CreatePlayer(Vector2f position, float speed, float growthFactor)
        {
            var playerImageData = Units.playerImage;
            var playerTexture = LoadTextureFromResource(playerImageData);

            var playerAnimator = new Animator(
                texture: playerTexture,
                frameWidth: 180,
                frameHeight: 180,
                totalFrames: 4,
                updateInterval: 0.1f  
            );

            return new Player(position, speed, growthFactor, playerAnimator);
        }
    }
}