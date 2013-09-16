using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bastion.Entity.Map;
using Bastion.Entity.Map.AStar;
using Bastion.Entity.Base;
using log4net;

namespace Bastion.Entity.Alive
{
    partial class Living : BaseCanLock
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Living));
        public Living()
        {
            Init_Routing();
            Init_Action();
        }


        internal void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Update_Action(gameTime);
        }

        public string Name { get; set; }
        public string TextureName { get; set; }

        public override string ToString()
        {
            return "Living [" + Name + "]";
        }
    }

}
