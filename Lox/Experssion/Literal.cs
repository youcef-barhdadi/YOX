using System;
using System.Collections.Generic;
using System.Text;

namespace Lox.Experssion
{
    public class Literal :Expr
    {
        Object value;
        public Literal(Object value)
        {
            this.value = value;
          
        }
    }
}
