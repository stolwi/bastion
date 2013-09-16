using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bastion.Entity.Map.AStar
{
    class AStarMap
    {
        Node[, ,] map;
        int xsize;
        int ysize;
        int zsize;
        Func<int, int, int, bool> traverseLambda;

        // Last parameter is a lambda taking three ints describing a node position (x, y, z), 
        // and returning a bool indicating if it is traversable.
        public AStarMap(int x, int y, int z, Func<int, int, int, bool> isTraversable)
        {
            xsize = x;
            ysize = y;
            zsize = z;
            map = new Node[xsize, ysize, zsize];
            traverseLambda = isTraversable;
        }

        public Node LookupNode(int x, int y, int z)
        {
            return map[x, y, z];
        }

        private bool IsNodeTraversable(int x, int y, int z)
        {
            // If it's a valid node, return if that node is traversable
            if (x >= 0 && x < xsize && y >= 0 && y < ysize && z >= 0 && z < zsize)
            {
                return map[x, y, z].Traversable;
            }
            // otherwise it's not a valid node, return false.
            return false;
        }


        public void CreateNodeArray()
        {
            // First loop and create all the basic nodes, with traversable if the basic block type is walkable
            for (int i = 0; i < xsize; i++)
            {
                for (int j = 0; j < ysize; j++)
                {
                    for (int k = 0; k < zsize; k++)
                    {
                        Position3 pos = new Position3(i, j, k);
                        map[i, j, k] = new Node { Position = pos, Traversable = traverseLambda(i, j, k) };
                    }
                }
            }
        }

        private void CreateNeighborList(int x, int y, int z)
        {
            Node me = map[x, y, z];
            List<Edge> oldNeighbors = me.Neighbors;
            me.Neighbors.Clear();
            int x1 = Math.Max(x - 1, 0);
            int x2 = Math.Min(x + 1, xsize - 1);
            int y1 = Math.Max(y - 1, 0);
            int y2 = Math.Min(y + 1, ysize - 1);
            int z1 = Math.Max(z - 1, 0);
            int z2 = Math.Min(z + 1, zsize - 1);
            for (int i = x1; i <= x2; i++)
            {
                for (int j = y1; j <= y2; j++)
                {
                    for (int k = z1; k <= z2; k++)
                    {
                        Node nbor = map[i, j, k];
                        if (me == nbor) continue; // skip self

                        bool canTraverse = nbor.Traversable;
                        int diagonalDegree = 0;

                        if (i != x)
                        {
                            diagonalDegree++;
                            canTraverse = canTraverse && map[x, j, k].Traversable; // check the one that is x-adjacent
                        }
                        if (j != y)
                        {
                            diagonalDegree++;
                            canTraverse = canTraverse && map[x, y, k].Traversable; // check the one that is y-adjacent
                        }
                        if (k != z)
                        {
                            diagonalDegree++;
                            canTraverse = canTraverse && map[x, j, z].Traversable; // check the one that is z-adjacent
                        }

                        // Ok, target node and any nodes adjacent are traversable - add as a neighbor
                        if (canTraverse)
                        {
                            me.Neighbors.Add(new Edge { /*Start = me,*/ End = nbor, Cost = GameConfig.Instance.DiagonalCostLookup[diagonalDegree] });
                        }
                    }
                }
            }
        }

        public void LoopCreateNeighborList()
        {
            // Now loop and see what walkable neighbors each node has
            // Check each of the 26 possible 3d neighbors
            for (int x = 0; x < xsize; x++)
            {
                for (int y = 0; y < ysize; y++)
                {
                    for (int z = 0; z < zsize; z++)
                    {
                        CreateNeighborList(x, y, z);
                    }
                }
            }
        }

        public void Reset()
        {
            for (int x = 0; x < xsize; x++)
            {
                for (int y = 0; y < ysize; y++)
                {
                    for (int z = 0; z < zsize; z++)
                    {
                        Node n = map[x, y, z];
                        if (n != null)
                        {
                            n.IsInClosedList = false;
                            n.IsInOpenList = false;
                            n.DistanceFromStart = float.MaxValue;
                            n.DistanceToTarget = float.MaxValue;
                        }
                    }
                }
            }
        }

        public float EstimateDistanceBetween(Position3 a, Position3 b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z);
        }

        internal void UpdatedBlock(int x, int y, int z)
        {
            // Block may be differently traversable after the update
            map[x, y, z].Traversable = traverseLambda(x, y, z);
            // TEST try updating every thing first, then work down to only updating what's needed
            //LoopCreateNeighborList();
            UpdateEdgesAround(x, y, z);
            //CreateNeighborList(x, y, z);
        }

        internal void UpdateEdgesAround(int x, int y, int z)
        {
            for (int i = Math.Max(0, x - 1); i < Math.Min(xsize, x + 1); i++)
            {
                for (int j = Math.Max(0, y - 1); j < Math.Min(ysize, y + 1); j++)
                {
                    for (int k = Math.Max(0, z - 1); k < Math.Min(zsize, z + 1); k++)
                    {
                        CreateNeighborList(i, j, k);
                    }
                }
            }
        }
    }
}
