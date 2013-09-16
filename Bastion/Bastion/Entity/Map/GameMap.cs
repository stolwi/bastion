using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Bastion.Entity.Map.AStar;
using Microsoft.Xna.Framework.Input;
using Bastion.UI;

namespace Bastion.Entity.Map
{
    class GameMap : Drawable
    {
        public int XSize { get { return xsize; } }
        public int YSize { get { return ysize; } }
        public int ZSize { get { return zsize; } }

        private int xsize;
        private int ysize;
        private int zsize;

        private MapSpot[, , ] board;

        public AStarPathFinder PathFinderWalk { get; set; }
        public bool BoardChanged { get; set; }

        public GameMap(int x, int y, int z)
        {
            xsize = x; 
            ysize = y;
            zsize = z;
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            board = new MapSpot[xsize, ysize, zsize];
            for (int x = 0; x < xsize; x++)
            {
                for (int y = 0; y < ysize; y++)
                {
                    for (int z = 0; z < zsize; z++)
                    {
                        board[x, y, z] = new MapSpot();
                    }
                }
            }
        }

        public void InitializePathFinding()
        {
            AStarMap mapWalk = new AStarMap(xsize, ysize, zsize, (i, j, k) => this.GetSpot(i, j, k).BT.Walkable);
            PathFinderWalk = new AStarPathFinder(mapWalk, (n1, n2) => mapWalk.EstimateDistanceBetween(n1.Position, n2.Position));
        }

        public MapSpot GetSpot(int x, int y, int z)
        {
            return board[x,y,z];
        }

        public void UpdateBlockType(int x, int y, int z, BlockType bt)
        {
            if (board[x, y, z].BT != bt)
            {
                board[x, y, z].BT = bt;
                // Consequences of changing the block type:
                PathFinderWalk.UpdatedBlock(x, y, z);
                BoardChanged = true;
            }
        }

        //public List<Position3> GetPath(Position3 startTile, Position3 endTile)
        //{
            
        //    return path.FindPath(StartPosition, FinishPosition);
        //}

        internal void FillSolidCube(int x1, int x2, int y1, int y2, int z1, int z2, BlockType bt)
        {
            for (int z = z1; z <= z2; z++)
            {
                for (int x = x1; x <= x2; x++)
                {
                    for (int y = y1; y <= y2; y++)
                    {
                        board[x, y, z].BT = bt;
                    }
                }
            }
        }

        internal void FillHollowRectangle(int x1, int x2, int y1, int y2, int z1, int z2, BlockType bt)
        {
            int z = z1; // no loop for now, assume one level.

            for (int x = x1; x <= x2; x++)
            {
                board[x, y1, z].BT = bt; // top
                board[x, y2, z].BT = bt; // bottom
            }
            for (int y = y1; y <= y2; y++)
            {
                board[x1, y, z].BT = bt; // left
                board[x2, y, z].BT = bt; // right
            }
        }
    }
}
