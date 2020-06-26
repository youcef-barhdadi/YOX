using System;
using System.Collections.Generic;
using System.Text;
using Lox.Experssion;

namespace Lox.Stmts
{
    public class Expression : Stmt
    {

        public Expr expression { get; set; }

        public Expression(Expr expression)
        {
            this.expression = expression;
        }

        public override T accept<T>(Visitor<T> visitor)
        {
            return visitor.visitExpressionStmt(this);
        }
    }
}
