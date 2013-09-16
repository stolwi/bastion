using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bastion.Entity.Map;

namespace Bastion.Entity
{
    class MapFillCommand
    {
        public bool SolidFill { get; set; }
        public Position3 FromPos { get; set; }
        public Position3 ToPos { get; set; }

        public BlockType BlockType { get; set; }
    }
}
