using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bastion.Entity.Map;
using Bastion.Entity.Map.AStar;
using System.Diagnostics;

namespace Bastion.Entity.Alive
{
    partial class Living
    {

        // TODO group these into some sort of "locomotion" control object
        public AStarPathFinder PathFinder { get; set; }
        public double MoveFraction { get; set; }
        public bool CanWalk { get; set; }

        public PrecisePosition3 Position { get; set; }
        private Position3 MoveTarget { get; set; }

        private void Init_Routing()
        {
            Position = new PrecisePosition3();
            Path = new Queue<Position3>();
        }

        private Queue<Position3> Path { get; set; }

        public void RecalcCurrentPath()
        {
            Path.Clear();
            PathTo(MoveTarget);
            if (Path.Count > 0)
            {
                // Often a recalc will add the current tile in.  
                // We want to skip the current tile and move to the next one in the list.
                if (Path.Peek().Equals(Position.TilePos))
                {
                    Path.Dequeue();
                }
            }
        }

        // Return true if we're at that point.
        public bool PathTo(Position3 target)
        {
            Debug.Assert(Path.Count == 0);

            if (Position.Pos.Equals(target))
            {
                // We're there
                NotifyArrivedAtWaypoint(target);
                target = null; // done
                return true;
            }
            MoveTarget = target;
            Position3 startTile = Position.TilePos;
            Position3 endTile = target.DivideBy(GameConfig.Instance.TileSize);

            if (startTile.Equals(endTile))
            {
                Path.Enqueue(endTile);
            }
            else
            {
                // Get a path, and add each element onto the Path queue.
                Stopwatch timing = new Stopwatch();
                timing.Start();
                PathFinder.FindPath(startTile, endTile).ForEach(x => Path.Enqueue(x));
                timing.Stop();
                log.InfoFormat("FindPath {0} to {1} took {2} ms", startTile, endTile, timing.Elapsed.ToString("c"));
                    
            }
            return false;
        }

        public void DoMovement(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Position3 destination = null;
            while (Path.Count > 0 && destination == null)
            {
                destination = Path.Peek().MultiplyBy(GameConfig.Instance.TileSize);
                if (destination != null && Position.Pos.Equals(destination))
                {
                    Path.Dequeue();
                    destination = null;
                }
            }
            if (destination != null)
            {
                double moveAmount = gameTime.ElapsedGameTime.TotalMilliseconds / MoveFraction;
                double dx1 = destination.X - Position.ExactPos.X;
                double dy1 = destination.Y - Position.ExactPos.Y;
                double dz1 = destination.Z - Position.ExactPos.Z;

                int diagonalDegree = 0;
                if (dx1 != 0) diagonalDegree++;
                if (dy1 != 0) diagonalDegree++;
                if (dz1 != 0) diagonalDegree++;
                double unitMove = moveAmount / GameConfig.Instance.DiagonalCostLookup[diagonalDegree];

                double dx = Math.Max(Math.Min(dx1, unitMove), -unitMove);
                double dy = Math.Max(Math.Min(dy1, unitMove), -unitMove);
                double dz = Math.Max(Math.Min(dz1, unitMove), -unitMove);

                Position.ExactMove(dx, dy, dz);
            }
        }
    }
}
