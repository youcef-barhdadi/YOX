using System;
using System.Collections.Generic;
using System.Text;
using Lox.Experssion;

namespace Lox.Stmts
{
    public class Print : Stmt
    {


        public Expr Expression { get; set; }

        public Print(Expr  ex)
        {
            this.Expression = ex;
        }
        public override T accept<T>(Visitor<T> visitor)
        {
            return visitor.visitPrintStmt(this);
        }
    }
}
