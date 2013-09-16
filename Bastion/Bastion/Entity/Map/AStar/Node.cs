using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bastion.Entity.Map.AStar
{
    internal class Node
    {
        public Position3 Position;
        public bool Traversable;
        public List<Edge> Neighbors;
        public Node Ancestor;
        public bool IsInClosedList;
        public bool IsInOpenList;
        public float DistanceToTarget;
        public float DistanceFromStart;

        public Node()
        {
            Neighbors = new List<Edge>();
        }
    }
}
