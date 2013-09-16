using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bastion.Entity.Alive
{
    interface ICanLock
    {
        List<ICanBeLocked> GetLocking();
        void Unlock(ICanBeLocked target);
        void UnlockAll();
    }
}
