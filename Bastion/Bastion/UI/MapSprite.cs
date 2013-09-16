using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bastion.Entity.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bastion.Entity;

namespace Bastion.UI
{
    class MapSprite
    {
        private GameMap map;
        private Rectangle bounds;

        public Rectangle Bounds { get { return bounds; } }
        public int TextureWidth { get { return GameConfig.Instance.TileSize; } }

        public int CurrentZLevel { get { return 1; } }
        
        public MapSprite(GameMap m)
        {
            map = m;
            bounds = new Rectangle(0, 0, TextureWidth * map.XSize, TextureWidth * map.YSize);
//            previouslyPressedTile = new Point(-1, -1); // special value means nothing was pressed.
//            selectedTile = new Point(-1, -1); // special value means nothing was pressed.
        }


        public void HandleSelect(MouseState mouseState, BlockType selectedBlockType)
        {
            Point p = new Point(mouseState.X / TextureWidth, mouseState.Y / TextureWidth);
            if (p.X < map.XSize && p.Y < map.YSize && CurrentZLevel < map.ZSize)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && selectedBlockType != null)
                {
                    map.UpdateBlockType(p.X, p.Y, CurrentZLevel, selectedBlockType);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
//            Texture2D backgroundTexture = TextureLibrary.GetTexture("BlankBackground");

            int z = 1; // for now just draw one level of the map
            for (int x = 0; x < map.XSize; x++)
            {
                for (int y = 0; y < map.YSize; y++)
                {
                    MapSpot spot = map.GetSpot(x, y, z);
                    BlockType bt = spot.BT;
                    Texture2D texture = bt.Texture;
                    if (texture != null)
                    {
                        Vector2 pos = new Vector2(x * TextureWidth, y * TextureWidth);
                        //Color color = (selectedTile.X == x && selectedTile.Y == y) ? Color.Yellow : Color.CornflowerBlue;
                        // Draw highlight if needed
                        //spriteBatch.Draw(backgroundTexture, pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                        spriteBatch.Draw(texture, pos, null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    }
                }
            }
//            selectedTile.X = -1; selectedTile.Y = -1; // turn off the highlight
        }
    }
}
