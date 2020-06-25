using Lox.Experssion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lox.AST
{

    // vistor patren
    public abstract  class Stmt
    {
        public abstract T Accept<T>(IVistor<T> visitor);

        public interface  IVistor<T> 
        {

            T visitGroupingExpr(Grouping t);
            T visitLiteralExpr(Literal t);
            T visitBinaryExpr(Binary t);
            T visitUnaryExpr(Unary t);
        }

        public class Ternary : Stmt
        {
            public Exception Predcate { get; set; }

            public override T Accept<T>(IVistor<T> visitor)
            {
                throw new NotImplementedException();
            }
        }
    }
}
