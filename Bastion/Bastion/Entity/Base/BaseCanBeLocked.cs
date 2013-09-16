using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bastion.Entity.Alive
{
    class BaseCanBeLocked : ICanBeLocked
    {
        private Living locker;

        public bool Lock(Living source)
        {
            if (locker == null)
            {
                locker = source;
                return true;
            }
            return false;
        }

        public void Unlock()
        {
            locker = null;
        }
    }
}
