using System;
using System.Collections.Generic;
using System.Text;
using Lox.Experssion;

namespace Lox.Stmts
{
   public class If  : Stmt
    {
        public Expr condation { get; set; }

        public Stmt thenBranch { get; set; }

        public Stmt elseBranch { get; set; }


        public If(Expr  exp, Stmt thenBranch, Stmt elseBranch)
        {
            this.condation = exp;
            this.elseBranch = elseBranch;
            this.thenBranch = thenBranch;
        }

        public override T accept<T>(Visitor<T> visitor)
        {
            return visitor.visitIfStmt(this);
        }
    }
}
