using System;
using System.Collections.Generic;
using System.Text;
using Lox.AST;

namespace Lox.Experssion
{
    public class Unary : Expr
    {

   

        public Expr Right { get; }
        public Token Operator { get; }

        public Unary(Expr right, Token tok)
        {
            this.Right = right;
            this.Operator = tok;
        }

        public override T accepte<T>(Stmt.IVistor<T> vistor)
        {
            return vistor.visitUnaryExpr(this);
        }
    }
}
