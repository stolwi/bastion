using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bastion.Entity.Map;
using Bastion.UI;
using Bastion.Entity;
using Bastion.Entity.Xml;
using Bastion.Entity.Alive;

namespace Bastion
{

    class GameConfig
    {
        readonly float diagonal1Cost = (float)Math.Sqrt(1);
        readonly float diagonal2Cost = (float)Math.Sqrt(2);
        readonly float diagonal3Cost = (float)Math.Sqrt(3);

        public float[] DiagonalCostLookup { get; set; }


        public List<BlockType> BlockTypes = new List<BlockType>();

        public int TileSize { get; set; }

        private static GameConfig instance = new GameConfig();

        public static GameConfig Instance { get { return instance; } }

        void LoadBlockTypes()
        {
            BlockTypes.AddRange(BlockTypeXml.ListFromXml("Data\\BlockTypes.xml"));
        }

        internal void AssignTextures()
        {
            foreach (BlockType bt in BlockTypes)
            {
                bt.Texture = TextureLibrary.GetTexture(bt.TextureName);
            }
        }

        public void Load()
        {
            TileSize = 16;
            LoadBlockTypes();

            DiagonalCostLookup = new float[4];
            DiagonalCostLookup[1] = diagonal1Cost;
            DiagonalCostLookup[2] = diagonal2Cost;
            DiagonalCostLookup[3] = diagonal3Cost;
        }

        internal GameMap LoadMap()
        {
            // Set the block types so that the Xml can translate the names to the actual blocktypes
            StartMapXml.blockTypes = BlockTypes;
            List<StartMap> startMaps = StartMapXml.ListFromXml("Data\\StartMap.xml");
            StartMap first = startMaps[0];

            GameMap map = new GameMap(first.XSize, first.YSize, first.ZSize);
            foreach (MapFillCommand mfc in first.FillCommands)
            {
                if (mfc.SolidFill)
                {
                    map.FillSolidCube(mfc.FromPos.X, mfc.ToPos.X, mfc.FromPos.Y, mfc.ToPos.Y, mfc.FromPos.Z, mfc.ToPos.Z, mfc.BlockType);
                }
                else
                {
                    map.FillHollowRectangle(mfc.FromPos.X, mfc.ToPos.X, mfc.FromPos.Y, mfc.ToPos.Y, mfc.FromPos.Z, mfc.ToPos.Z, mfc.BlockType);
                }
            }
            map.InitializePathFinding();
            return map;
        }

        internal List<Living> LoadLivings()
        {
            return LivingXml.ListFromXml("Data\\Living.xml");
        }
    }
}
