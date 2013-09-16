using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bastion.Entity.Map;

namespace Bastion.Entity.Alive
{
    partial class Living
    {

        Queue<Action> actions;

        void Init_Action()
        {
            actions = new Queue<Action>();
        }

        public void AddAction(Action act)
        {
            actions.Enqueue(act);
        }

        void NotifyArrivedAtWaypoint(Position3 destination)
        {
            if (actions.Count > 0)
            {
                Action next = actions.Peek();
                if (next is MoveAction)
                {
                    ((MoveAction)next).CheckDestination(destination);
                }
            }
        }

        void Update_Action(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (actions.Count > 0)
            {
                Action next = actions.Peek();
                // If update returns true, then the action is finished
                if (next.Update(this, gameTime))
                {
                    actions.Dequeue();
                    // Note that the next action won't do anything until the next update cycle
                }
            }
        }

    }
}
