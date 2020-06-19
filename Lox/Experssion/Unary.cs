using System;
using System.Collections.Generic;
using System.Text;

namespace Lox.Experssion
{
    public class Unary : Expr
    {

        Expr right;
        Token _operator;
        public Unary(Expr right, Token tok)
        {
            this.right = right;
            this._operator = tok;
        }

        public override Expr accepte(IVistor<Expr> vistor)
        {
            return vistor.vist(this);
        }
    }
}
