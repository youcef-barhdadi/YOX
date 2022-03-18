using System;
using System.Collections.Generic;
using System.Text;
using Lox.Experssion;

namespace Lox.Stmts 
{
    public class While : Stmt
    {
        public While(Expr condition, Stmt body)
        {
            Condition = condition;
            Body = body;
        }
        //public While()
        //{

        //}
        public Expr Condition { get; set; }
        public Stmt Body { get; set; }

        public override T accept<T>(Visitor<T> visitor)
        {
           return  visitor.visitWhileStmt(this);
        }


        //public override T accept<T>(Visitor<T> visitor)
        //{
        //    visitor.visit
        //}
    }
}
