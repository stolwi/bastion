using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Bastion.Entity.Map;
using Bastion.Entity;

namespace Bastion.UI
{
    class BlockButton : Drawable
    {

        public BlockType BlockType { get; set; }
//        private MouseState previousState;
        private Texture2D textureBack;

        private bool isSelected = false;
         
        private Rectangle bounds;

        public BlockButton(BlockType bt, Vector2 pos, float scale)
        {
            BlockType = bt;
            Position = pos;
            Scale = new Vector2(scale, scale);

        }

        public void SetSelected(bool t)
        {
            isSelected = t;
        }

        public void InitTextures()
        {
            Texture = BlockType.Texture;
            textureBack = TextureLibrary.GetTexture("BlankBackground");
            bounds = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color color = Color.White;
            // First draw the background, indicating whether selected or not:
            if (isSelected)
            {
                if (/*Level.Instance.Player.PlayerGold >= BlockType.Cost*/ true)
                {
                    color = Color.Yellow;
                }
                else
                {
                    color = Color.Red;
                }
            }
            else
            {
                color = Color.CornflowerBlue;
            }
            spriteBatch.Draw(textureBack, Position, null, color, 0f, Offset, Scale, SpriteEffects.None, 0f);

            base.Draw(spriteBatch);
        }


        internal void Update(GameTime gameTime)
        {
            
        }
    }
}
