using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bastion.Entity.Alive
{
    interface ICanBeLocked
    {
        bool Lock(Living source);

        void Unlock();
    }
}
