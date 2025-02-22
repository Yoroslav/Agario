using SFML.Graphics;
using System.IO;

namespace Agario.Project.Game.MenuSkins
{
    public static class ResourceManager
    {
        public static Texture GetSkinTexture(string skinName)
        {
            string path = Path.Combine("Assets", "Skins", $"{skinName}.png");
            return new Texture(path);
        }

        public static Texture GetUITexture(string name)
        {
            string path = Path.Combine("Assets", "UI", $"{name}.png");
            return new Texture(path);
        }

        public static Font GetFont()
        {
            string path = Path.Combine("Assets", "arial.ttf");
            return new Font(path);
        }
    }
}