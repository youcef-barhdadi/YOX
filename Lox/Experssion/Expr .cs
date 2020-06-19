using Lox.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lox.Experssion
{


    public abstract class Expr
    {


        public abstract T accepte<T>(IExprVisitor<T> vistor);

    }
}
