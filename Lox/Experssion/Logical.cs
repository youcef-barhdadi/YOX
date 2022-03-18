using System;
using System.Collections.Generic;
using System.Text;
using Lox.AST;
namespace Lox.Experssion
{
    public class Logical : Expr
    {
        Expr left;
        public Expr Left { get; set; }
        public Expr Right { get; set; }
        public Token Operator { get; set; }


        public Logical(Expr left, Token oper, Expr right)
        {
            this.Left = left;
            this.Right = right;
            this.Operator = oper;
        }

        public override T accepte<T>(Expr.IVistor<T> vistor)
        {
            return vistor.visitLogicalExpr(this);
        }
    }
}
