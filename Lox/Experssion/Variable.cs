using System;
using System.Collections.Generic;
using System.Text;

namespace Lox.Experssion
{
    public class Variable : Expr
    {
        public Token Name { get; set; }
        public Variable(Token name)
        {
            this.Name = name;
        }
        public override T accepte<T>(IVistor<T> vistor)
        {
            return vistor.visitVariableExpr(this);
        }
    }
}
