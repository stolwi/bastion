using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bastion.Entity.Resource
{
    class ResourceManager
    {

        List<Resource> resources;

        public ResourceManager()
        {
            resources = new List<Resource>();
        }

        public void AddResource(Resource r)
        {
            resources.Add(r);
        }

        void UpdateResources(Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (Resource r in resources)
            {
                r.Update(gameTime);
            }
        }

    }
}
