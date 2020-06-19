using System;
using System.Collections.Generic;
using System.Text;

namespace Lox.Experssion
{
   public class Binary : Expr
    {
        protected Expr left;
        protected Expr right;
        protected Token _operator;
        public Binary(Expr left, Token Operator, Expr right)
        {
            this.left = left;
            this._operator = Operator;
            this.right = right;
        }
    }
}
