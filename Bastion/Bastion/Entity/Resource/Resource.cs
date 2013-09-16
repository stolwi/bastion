using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bastion.Entity.Map;

namespace Bastion.Entity.Resource
{
    class Resource
    {
        Position3 Position { get; set;}
        string Name { get; set; }

        internal virtual void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            
        }
    }
}
