using System;
using System.Collections.Generic;
using System.Text;

namespace Lox.Experssion
{
     public class Grouping : Expr
    {
        Expr expression;


        public Grouping(Expr exprssion)
        {
            this.expression = exprssion;
        }

        public override T accepte<T>(IExprVisitor<T> vistor)
        {
            return  vistor.vistGroup(this);
        }
    }
}
