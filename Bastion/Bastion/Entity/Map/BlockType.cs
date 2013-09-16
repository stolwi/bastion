using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;
using System.Xml;

namespace Bastion.Entity
{
    public class BlockType
    {
        public int BlockTypeId { get; set; }
        public string Name { get; set; }
//        bool Flyable { get; set; }
        public bool Walkable { get; set; }
//        bool Driveable { get; set; }
//        bool Swimmable { get; set; }

        public string TextureName { get; set; }
        public Texture2D Texture { get; set; }

        public override string ToString()
        {
            return "BT: " + Name;
        }

    }
}
