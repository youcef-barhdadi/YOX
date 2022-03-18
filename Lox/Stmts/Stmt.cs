using System;
using System.Collections.Generic;
using System.Text;

namespace Lox.Stmts
{
    abstract public class Stmt
    {
        public interface Visitor<T>
        {
            T visitPrintStmt(Print stmt);
            T visitExpressionStmt(Expression stmt);
            T visitVarStmt(Var stmt);
            T visitBlockStmt(Block stmt);
            T visitIfStmt(If stmt);
            T visitWhileStmt(While stmt);


        }
        public abstract T accept<T>(Stmt.Visitor<T> visitor);
    }
}
