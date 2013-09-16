using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bastion.Entity;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Bastion.Entity.Alive;

namespace Bastion.UI
{
    class LivingSprite
    {
        const int NameOffsetX = 5;
        const int NameOffsetY = -12;

        Living living;

        Texture2D Texture { get; set; }
        SpriteFont Font { get; set; }

        int TextureWidth { get { return GameConfig.Instance.TileSize; } }

        public LivingSprite(Living liv)
        {
            living = liv;
        }

        public void InitTexture()
        {
            Texture = TextureLibrary.GetTexture("X");
            Font = TextureLibrary.GetFont("Arial");
        }
            

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                Vector2 pos = new Vector2(living.Position.Pos.X, living.Position.Pos.Y);
                spriteBatch.Draw(Texture, pos, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            }
            if (living.Name != null)
            {
                Vector2 namePos = new Vector2(living.Position.Pos.X + NameOffsetX, living.Position.Pos.Y + NameOffsetY);
                spriteBatch.DrawString(Font, living.Name, namePos, Color.White);
            }
        }


        internal void Update(GameTime gameTime)
        {
            // do nothing for now.  Eventually this will animate the living object
        }
    }
}
