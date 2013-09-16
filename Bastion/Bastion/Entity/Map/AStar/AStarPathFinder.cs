using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Bastion.ExtensionMethods;
using Bastion.Exceptions;

namespace Bastion.Entity.Map.AStar
{
    class AStarPathFinder
    {
//        private static readonly ILog log = LogManager.GetLogger(typeof(AStarPathFinder));

        private List<Node> openList;
        Func<Node, Node, float> estimateDistance;
        private AStarMap nodeMap;

//        private List<Node> closedList;

        public AStarPathFinder(AStarMap map, /*int s,*/ Func<Node, Node, float> distanceCalc)
        {
            nodeMap = map;
            estimateDistance = distanceCalc;
            openList = new List<Node>();
            // These two should be done only once, unless the map changes.
            // TODO figure out smart re-calculating of neighbors when a node changes
            nodeMap.CreateNodeArray();
            nodeMap.LoopCreateNeighborList();
        }

        private void AddToOpenList(Node n)
        {
            n.IsInOpenList = true;
            openList.Add(n);
        }

        private void AddToClosedList(Node n)
        {
            n.IsInClosedList = true;
        }

        private void RemoveFromOpenList(Node n)
        {
            n.IsInOpenList = false;
            openList.Remove(n);
        }

        // position3's coming in are world coordinates, precise positions
        public List<Position3> FindPath(Position3 start, Position3 end)
        {
//            log.DebugFormat("FindPath({0}, {1}", start, end);
            if (start.Equals(end)) return new List<Position3>();
            Reset();

            Node startNode = nodeMap.LookupNode(start.X, start.Y, start.Z);
            Node endNode = nodeMap.LookupNode(end.X, end.Y, end.Z);

            startNode.DistanceToTarget = nodeMap.EstimateDistanceBetween(start, end);
            startNode.DistanceFromStart = 0;
            AddToOpenList(startNode);

            while (openList.Count > 0)
            {
                Node current = FindClosestToTarget();

                if (current == null)
                {
                    throw new PathException("No Path to Target");
                }
                if (current == endNode)
                {
                    return TracePath(startNode, endNode);
                }
                foreach (Edge edge in current.Neighbors)
                {
                    // Shouldn't need these checks due to the way the list was set up,
                    // but assert them anyway.
                    Node neighbor = edge.End;
                    Debug.Assert(neighbor != null);
                    if (neighbor.Traversable)
                    {
                        if (!neighbor.IsInClosedList && !neighbor.IsInOpenList)
                        {
                            neighbor.DistanceFromStart = current.DistanceFromStart + edge.Cost;
                            neighbor.DistanceToTarget = neighbor.DistanceFromStart + nodeMap.EstimateDistanceBetween(neighbor.Position, end);
                            neighbor.Ancestor = current;
                            AddToOpenList(neighbor);
                        }
                        else if (neighbor.IsInClosedList || neighbor.IsInOpenList)
                        {
                            if (neighbor.DistanceFromStart > current.DistanceFromStart + edge.Cost)
                            {
                                // Whatever path was used for neighbor before, now we have a closer path through current
                                neighbor.DistanceFromStart = current.DistanceFromStart + edge.Cost;
                                neighbor.DistanceToTarget = neighbor.DistanceFromStart + nodeMap.EstimateDistanceBetween(neighbor.Position, end);
                                neighbor.Ancestor = current;
                            }
                        }
                    }
                }
                RemoveFromOpenList(current);
                AddToClosedList(current);
            }
            return new List<Position3>();
        }

        private void Reset()
        {
//            log.Debug("AStarPathFinder reset");
            openList.Clear();
            nodeMap.Reset();
        }


        private Node FindClosestToTarget()
        {
            if (openList.Count == 0) return null;
            Node n = openList[0];
            float shortestDistance = n.DistanceToTarget;
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].DistanceToTarget < shortestDistance)
                {
                    n = openList[i];
                    shortestDistance = n.DistanceToTarget;
                }
            }
            return n;
        }

        private List<Position3> TracePath(Node start, Node end)
        {
            List<Position3> lst;
            if (start == end)
            {
                // tail of the recursion
                lst = new List<Position3>();
            }
            else
            {
                // First recurse down to the point where we've traced end all the way
                // back up to the start node
                lst = TracePath(start, end.Ancestor);
            }
            // Then as we unwind, we add new vector2's to the list, in effect we're
            // moving forward down the list that we only had a backtrace to!
            lst.Add(end.Position);
            return lst;
        }

        // target block has been updated - now we have to recalculate neighbors!
        internal void UpdatedBlock(int x, int y, int z)
        {
            nodeMap.UpdatedBlock(x, y, z);
        }
    }
}
