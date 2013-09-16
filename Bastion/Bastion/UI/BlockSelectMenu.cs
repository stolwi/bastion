using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Bastion.Entity.Map;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bastion.Entity;

namespace Bastion.UI
{
    class BlockSelectMenu
    {
        const int xMax = 2;
        const int yMax = 5;
        int xOffset;
        int yOffset;
        const int blockSpriteWidth = 32;

        public List<BlockButton> blockButtons { get; set; }
        private BlockButton[,] grid = new BlockButton[xMax, yMax];
        private BlockButton previouslyPressedOnBlock = null;
        private BlockButton selectedBlock = null;

        public BlockType SelectedBlockType 
        { 
            get 
            {
                if (selectedBlock == null) return null;
                return selectedBlock.BlockType;
            }
        }

        private Rectangle bounds;
        public Rectangle Bounds { get { return bounds; } }

        public BlockSelectMenu(int x, int y, GameConfig config)
        {
            int xPos = 0;
            int yPos = 0;

            xOffset = x;
            yOffset = y;

            blockButtons = new List<BlockButton>();
            bounds = new Rectangle(xOffset, yOffset, blockSpriteWidth * xMax, blockSpriteWidth * yMax);
            
            foreach (BlockType bt in config.BlockTypes)
            {
                Vector2 pos = new Vector2(xPos * blockSpriteWidth + xOffset, yPos * blockSpriteWidth + yOffset);
                BlockButton button = new BlockButton(bt, pos, .5F);
                grid[xPos, yPos] = button; // Helps us locate mouseclicks
                xPos++;
                if (xPos == xMax)
                {
                    yPos++;
                    xPos = 0;
                }
                System.Diagnostics.Debug.Assert(yPos < yMax);
                blockButtons.Add(button);
            }
        }

        public void InitTextures()
        {
            foreach (BlockButton bb in blockButtons)
            {
                bb.InitTextures();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (BlockButton bb in blockButtons)
            {
                bb.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (BlockButton bb in blockButtons)
            {
                bb.Update(gameTime);
            }
        }

        internal void HandleSelect(MouseState mouseState)
        {
            int x = (mouseState.X - xOffset) / blockSpriteWidth;
            int y = (mouseState.Y - yOffset) / blockSpriteWidth;
            if (x >= 0 && y >= 0 && x < xMax && y < yMax && grid[x, y] != null)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    previouslyPressedOnBlock = grid[x, y];
                }
                else if (mouseState.LeftButton == ButtonState.Released && previouslyPressedOnBlock == grid[x, y])
                {
                    //                        if (Level.Instance.Player.PlayerGold >= previouslyPressedOnBlock.TowerType.Cost)
                    {
                        if (selectedBlock != null)
                        {
                            selectedBlock.SetSelected(false);
                        }
                        selectedBlock = previouslyPressedOnBlock;
                        grid[x, y].SetSelected(true);
                    }
                }
            }
            if (mouseState.LeftButton == ButtonState.Released)
            {
                previouslyPressedOnBlock = null;
            }
        }
    }
}
