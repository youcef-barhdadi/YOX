using System;
using System.Collections.Generic;
using System.Text;
using Lox.AST;

namespace Lox.Experssion
{
     public  interface IExprVisitor <T>
    {
        T vistGroup(Grouping vistor);
    }
}
