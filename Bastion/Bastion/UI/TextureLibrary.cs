using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Bastion.UI
{
    class TextureLibrary
    {
        static Dictionary<string, Texture2D> repository = new Dictionary<string, Texture2D>();
        static Dictionary<string, SpriteFont> fontRepo = new Dictionary<string, SpriteFont>();
        static bool isInitialized = false;

        internal static void LoadTextures(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            repository.Add("Air", Content.Load<Texture2D>("Blocks/Air"));
            repository.Add("Dirt", Content.Load<Texture2D>("Blocks/Dirt"));
            repository.Add("Stone", Content.Load<Texture2D>("Blocks/Stone"));
            repository.Add("Surface", Content.Load<Texture2D>("Blocks/Surface"));
            repository.Add("Water", Content.Load<Texture2D>("Blocks/Water"));
            repository.Add("X", Content.Load<Texture2D>("Livings/X"));
            repository.Add("BlankBackground", Content.Load<Texture2D>("UI/BlankBackground"));
            isInitialized = true;
        }

        internal static void LoadFonts(Microsoft.Xna.Framework.Content.ContentManager Content)
        {
            fontRepo.Add("Arial", Content.Load<SpriteFont>("Font\\Arial"));
        }

        public static Texture2D GetTexture(string name)
        {
            if (!isInitialized) return null;
            return repository[name];
        }
        public static SpriteFont GetFont(string name)
        {
            if (!isInitialized) return null;
            return fontRepo[name];
        }
    }
}
