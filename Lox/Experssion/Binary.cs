using Lox.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lox.Experssion
{
   public class Binary : Expr
    {
  
        public Expr Left { get;}
        public Expr Right { get; }
        public Token Operator { get; }

        public Binary(Expr left, Token Operator, Expr right)
        {
            this.Left = left;
            this.Operator = Operator;
            this.Right = right;
        }

        public override T accepte<T>(Stmt.IVistor<T> vistor)
        {
            return vistor.visitBinaryExpr(this);
        }
    }
}
