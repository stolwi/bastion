using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bastion.Entity.Alive
{
    enum ActionStatus { Start, Success, Failure, NoAction, InProgress };
    class Action
    {

        public Action()
        {
            Status = ActionStatus.Start;
        }

        public ActionStatus Status { get; set; }
        // Return true if the action is completed.
        public virtual bool Update(Living actor, Microsoft.Xna.Framework.GameTime gameTime)
        {
            Status = ActionStatus.NoAction;
            return true;
        }
    }
}
