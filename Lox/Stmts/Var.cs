using System;
using System.Collections.Generic;
using System.Text;
using Lox.Experssion;

namespace Lox.Stmts
{
    public class Var : Stmt
    {

        public Token Name { get; set; }
        public Expr Initializer { get; set; }


        public Var(Token name, Expr ini)
        {
            this.Name = name;
            this.Initializer = ini;
        }
        public override T accept<T>(Visitor<T> visitor)
        {
            return visitor.visitVarStmt(this);
        }
    }
}
