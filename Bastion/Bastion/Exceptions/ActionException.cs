using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bastion
{
    class ActionException : Exception
    {
        public ActionException(string str) 
            : base(str)
        {

        }

        public ActionException(string str, Exception inner)
            : base(str, inner)
        {
        }
    }
}
