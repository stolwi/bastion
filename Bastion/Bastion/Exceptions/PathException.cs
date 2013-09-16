using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bastion.Exceptions
{
    class PathException : Exception
    {
        public PathException(string str) 
            : base(str)
        {

        }

        public PathException(string str, Exception inner)
            : base(str, inner)
        {

        }
    }
}
