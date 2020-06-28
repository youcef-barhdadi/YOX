using System;
using System.Collections.Generic;
using System.Text;

namespace Lox.Experssion
{
    public class Assign : Expr
    {

        public Token Name { get; set; }
        public Expr Value { get; set; }


        public Assign(Token  name, Expr value)
        {
            this.Name = name;
            this.Value = value;
        }
        public override T accepte<T>(IVistor<T> vistor)
        {
            return vistor.visitAssignExpr(this);
        }
    }
}
