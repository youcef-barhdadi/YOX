using System;
using System.Collections.Generic;
using System.Text;

namespace Lox.Stmts
{
   public class Block : Stmt
    {
        public List<Stmt> statements { get; set; }
        public override T accept<T>(Visitor<T> visitor)
        {
           return visitor.visitBlockStmt(this);
        }
        public Block(List<Stmt> statements)
        {
            this.statements = statements;
        }
    }
}
