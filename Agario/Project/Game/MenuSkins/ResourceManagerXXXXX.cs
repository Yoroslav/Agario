using SFML.Graphics;
using System;
using System.Resources;
using System.IO;

namespace Agario.Project.Game.MenuSkins
{
    public static class ResourceManagerXXXXX
    {
        private static Font _fontCache;

        public static Texture GetSkinTexture(string skinName)
        {
            System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("Agario.Units", typeof(Program).Assembly);
            var obj = resourceManager.GetObject(skinName);
            return new Texture((byte[])obj);
        }

        public static Texture GetUITexture(string name)
        {
            System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("Agario.Ui", typeof(Program).Assembly);
            var obj = resourceManager.GetObject(name);
            return new Texture((byte[])obj);
        }

        public static Font GetFont()
        {
            System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("Agario.Fonts", typeof(Program).Assembly);
            var obj = resourceManager.GetObject("arial");
            return new Font((byte[])obj);
        }
        public static Texture GetDefaultSkin()
        {
            try
            {
                var resourceManager = new ResourceManager("Agario.Units", typeof(Program).Assembly);
                object obj = resourceManager.GetObject("default_skin");

                if (obj == null) throw new FileNotFoundException("Default skin resource not found");

                return new Texture((byte[])obj);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to load default skin", ex);
            }
        }
    }
}
