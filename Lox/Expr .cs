using System;
using System.Collections.Generic;
using System.Text;

namespace Lox
{

     class Binary : Expr
    {
        Binary(Expr left, Token Operator, Expr right)
        {
            this.left = left;
            this._operator = Operator;
            this.right = right;
        }

    }
    public abstract class Expr
    {


     



       protected Expr left;
       protected Expr right;
       protected Token _operator;

    }
}
