using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bastion.UI;
using Microsoft.Xna.Framework;
using Bastion.Entity.Map;
using Bastion.Entity.Alive;

namespace Bastion.Entity
{
    class World
    {
        private GameMap map;
        public GameMap Map { get { return map; } }
        public bool TimePaused { get; set; } 

        public List<Living> Livings { get; set; }

        public Gui Gui { get; set; }

        private GameConfig config;

        public World(GameConfig c)
        {
            config = c;
            map = config.LoadMap();
            LoadLivings();
            TimePaused = false;
        }

        private void LoadLivings()
        {
            Livings = config.LoadLivings();
            foreach (Living living in Livings)
            {
                InitVillager(living);
            }
        }

        public void InitVillager(Living villager)
        {
            if (villager.CanWalk)
            {
                villager.PathFinder = map.PathFinderWalk;
                villager.MoveFraction = 50;
            }
            villager.AddAction(new MoveAction(40 * config.TileSize, 24, 1));
            //villager.AddRoute(40 * config.TileSize, 40 * config.TileSize, 1);
            //villager.AddRoute(8 * config.TileSize, 37 * config.TileSize, 1);
            //villager.AddRoute(8 * config.TileSize, 25 * config.TileSize, 1);
        }

        internal void Update(GameTime gameTime)
        {
            if (map.BoardChanged)
            {
                foreach (Living l in Livings)
                {
                    l.RecalcCurrentPath();
                }
                map.BoardChanged = false;
            }
            if (!TimePaused)
            {
                foreach (Living l in Livings)
                {
                    l.Update(gameTime);
                }
            }
        }
    }
}
