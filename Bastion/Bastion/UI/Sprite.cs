using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bastion.UI
{
    public class Sprite
    {
        virtual protected Texture2D Texture { get; set; }
        virtual protected Vector2 Offset { get { return offset; } set { offset = value; } }

        virtual protected float Rotation { get; set; }

        virtual protected Vector2 Scale { get; set; }

        virtual protected Color DrawColor { get; set; }

        private Vector2 offset = Vector2.Zero;

        public virtual void InitTexture(string textureName)
        {
            Texture = TextureLibrary.GetTexture(textureName);
            DrawColor = Color.White;
        }

        public int GetTextureWidth() { return (Texture != null) ? Texture.Width : 0; }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 Position)
        {
            if (Texture != null)
            {
                if (Scale != null && Scale != Vector2.Zero)
                {
                    spriteBatch.Draw(Texture, Position, null, DrawColor, 0f, Offset, Scale, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(Texture, Position, null, DrawColor, 0f, Offset, 1f, SpriteEffects.None, 0f);
                }
            }
        }

        internal void CenterOffset()
        {
            Offset = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }

        // Draw to specific dimensions with a specific color
        internal void Draw(SpriteBatch spriteBatch, Rectangle destination, Color color)
        {
            spriteBatch.Draw(Texture, destination, color);
        }

        internal int GetTextureHeight()
        {
            return (Texture != null) ? Texture.Height : 0;
        }
    }
}
