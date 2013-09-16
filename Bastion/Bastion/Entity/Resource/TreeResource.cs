using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bastion.Entity.Resource
{
    class TreeResource : Resource
    {
        long age;

        internal override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            age += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }
}
