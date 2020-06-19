using Lox.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lox.Experssion
{
    public class Literal :Expr
    {
       // Object value;

        public object Value { get;}


        public Literal(Object value)
        {
            this.Value = value;
          
        }

        public override T accepte<T>(Stmt.IVistor<T> vistor)
        {
            return vistor.visitLiteralExpr(this);
        }
    }
}
