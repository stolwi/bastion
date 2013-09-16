using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bastion.UI
{
    public class Drawable
    {
        public Vector2 Position { get; set; }
        virtual protected Texture2D Texture { get; set; }
        virtual protected Vector2 Offset { get { return offset; } set { offset = value; } }

        virtual protected float Rotation { get; set; }

        virtual protected Vector2 Scale { get; set; }

        private Vector2 offset = Vector2.Zero;

        public virtual void Initialize(string textureName)
        {
            Texture = TextureLibrary.GetTexture(textureName);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                if (Scale != null && Scale != Vector2.Zero)
                {
                    spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Offset, Scale, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Offset, 1f, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
