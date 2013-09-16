using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bastion.Entity.Map;
using Bastion.Exceptions;

namespace Bastion.Entity.Alive
{

    // TODO NOTE: Maybe we need a list of queued actions.
    // The movement process can allow subscribers.  when we arrive at a tile point, a notification is posted to the subscribers.
    // Any Moveaction subscribers can then complete() themselves when arriving at the designated tile.
    class MoveAction : Action
    {

        public MoveAction(Position3 target)
        {
            MoveTarget = target;
        }

        public MoveAction(int x, int y, int z)
        {
            MoveTarget = new Position3(x, y, z);
        }

        public Position3 MoveTarget { get; set; }

        public override bool Update(Living actor, Microsoft.Xna.Framework.GameTime gameTime)
        {
            try
            {
                if (Status == ActionStatus.Start)
                {
                    actor.PathTo(MoveTarget);
                    Status = ActionStatus.InProgress;
                }
                actor.DoMovement(gameTime);
            }
            catch (PathException)
            {
                Status = ActionStatus.Failure;
            }
            return (Status != ActionStatus.InProgress);
        }

        internal void CheckDestination(Position3 destination)
        {
            if (destination.Equals(MoveTarget))
            {
                Status = ActionStatus.Success;
            }
        }
    }
}
