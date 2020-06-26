using Lox.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lox.Experssion
{
    public class Ternary : Expr
    {
        public Expr Predcate { get; set; }
        public Expr expre1 { get; set; }
        public Expr expre2 { get; set; }

        public Ternary(Expr Predcate, Expr e1, Expr e2)
        {
            this.Predcate = Predcate;
            this.expre1 = e1;
            this.expre2 = e2;
        }

        public override T accepte<T>(Expr.IVistor<T> vistor)
        {
            return vistor.visitTernaryExpr(this);
        }
    }
}
