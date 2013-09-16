using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bastion.Entity
{
    class StartMap
    {
            /*int defZ = 1; // default z-level for now
            int maxX = MapWidth - 1;
            int maxY = MapHeight - 1;

            BlockType btAir = BlockTypes.Find(x => x.Name.Equals("Air"));
            BlockType btStone = BlockTypes.Find(x => x.Name.Equals("Stone"));
            BlockType btSurface = BlockTypes.Find(x => x.Name.Equals("Surface"));
            BlockType btWater = BlockTypes.Find(x => x.Name.Equals("Water"));
            BlockType btDirt = BlockTypes.Find(x => x.Name.Equals("Dirt"));

            //map.FillSolidCube(0, maxX, 0, maxY, 0, MapDepth - 1, btSurface); // Surface to everything

            map.FillSolidCube(0, maxX, 0, maxY, 0, MapDepth - 1, btAir); // Air to everything
            map.FillSolidCube(0, maxX, 0, maxY, 1, 1, btSurface); // "Surface" on level 1
            map.FillHollowRectangle(0, maxX, 0, maxY, defZ, defZ, btStone);

            map.FillSolidCube(10, 25, 30, 45, 1, 1, btWater);
            map.FillHollowRectangle(9, 26, 29, 46, 1, 1, btDirt);
            map.FillHollowRectangle(26, 26, 10, 28, 1, 1, btDirt);
            map.FillHollowRectangle(26, 40, 28, 28, 1, 1, btDirt);
            
            map.FillHollowRectangle(35, 35, 3, 27, 1, 1, btDirt);
            map.FillHollowRectangle(30, 30, 1, 22, 1, 1, btDirt);

            map.FillHollowRectangle(1, 8, 30, 30, 1, 1, btDirt);
            
            map.InitializePathFinding(); */

        public string Name { get; set; }
        public int XSize { get; set; }
        public int YSize { get; set; }
        public int ZSize { get; set; }

        public int DefaultZ { get; set; }

        public BlockType DefaultBlockType { get; set; }

        public List<MapFillCommand> FillCommands { get; set; }
    }
}
