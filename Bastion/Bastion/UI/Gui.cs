using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Bastion.Entity.Map;
using Bastion.Entity;
using Bastion.Entity.Alive;

namespace Bastion.UI
{
    class Gui : Drawable
    {

        public MapSprite MapSprite { get; set; }
        private List<LivingSprite> livingSprites = new List<LivingSprite>();
        private BlockSelectMenu blockMenu;
        private World world;

        GameConfig config;

        public Gui(World w, GameConfig c)
        {
            config = c;
            MapSprite = new MapSprite(w.Map);
            world = w;
            world.Gui = this;
            blockMenu = new BlockSelectMenu(MapSprite.Bounds.Width, 0, c);
            foreach (Living living in world.Livings)
            {
                AddLivingSprite(living);
            }
        }
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;

        private MouseState mouseState;

        public int GetWindowHeight() { return MapSprite.Bounds.Height; }
        public int GetWindowWidth() { return MapSprite.Bounds.Width + blockMenu.Bounds.Width; }

        public void InitializeTextures()
        {
            config.AssignTextures();
            foreach (LivingSprite ls in livingSprites)
            {
                ls.InitTexture();
            }
            blockMenu.InitTextures();
        }

        public void AddLivingSprite(Living l)
        {
            livingSprites.Add(new LivingSprite(l));
        }

        public void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyUp(Keys.Space) && previousKeyboardState.IsKeyDown(Keys.Space))
            {
                world.TimePaused = !world.TimePaused;
            }
            blockMenu.Update(gameTime);
            mouseState = Mouse.GetState();
            if (blockMenu.Bounds.Contains(mouseState.X, mouseState.Y))
            {
                blockMenu.HandleSelect(mouseState);
            }

            if (MapSprite.Bounds.Contains(mouseState.X, mouseState.Y))
            {
                MapSprite.HandleSelect(mouseState, blockMenu.SelectedBlockType);
            }

            foreach (LivingSprite ls in livingSprites)
            {
                ls.Update(gameTime);
            }
            // Save this state for comparison next time
            previousKeyboardState = currentKeyboardState;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            MapSprite.Draw(spriteBatch);
            foreach (LivingSprite ls in livingSprites)
            {
                ls.Draw(spriteBatch);
            }
            blockMenu.Draw(spriteBatch);
        }

    }
}
