using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bastion.Entity.Alive
{
    // Activity has a series of Actions that are executed in order.
    // If an action fails, the activity is generally aborted.
    // Example: Chop Lumber involves:
    //  Find Tree resource
    //  Lock tree Resource (if fails, go back to previous step)
    //  Move to Tree resource
    //  Chop tree  (creates a log)
    //  Unlock all
    // Example: Create Carpentry workshop
    //  Lock placement location
    //  Find/Lock/MoveTo/Pickup log
    //  Work time...
    //  Find/Lock/MoveTo/Pickup log
    //  Work time...
    //  (creates a workshop entity)
    //  Pick up workshop entity
    //  MoveTo placement location
    //  Place workshop
    //  Unlock all    
    class Activity
    {

    }
}
