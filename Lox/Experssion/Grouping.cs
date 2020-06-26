using System;
using System.Collections.Generic;
using System.Text;
using Lox.AST;
namespace Lox.Experssion
{
    public class Grouping : Expr
    {
        Expr expression;

        public Expr Experssion
        {
            get { return this.expression; } 
        }

        public Grouping(Expr exprssion)
        {
            this.expression = exprssion;
        }

        public override T accepte<T>(Expr.IVistor<T> vistor)
        {
            return vistor.visitGroupingExpr(this);
        }
    }


 
}
