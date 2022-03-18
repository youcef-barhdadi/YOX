using Lox.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lox.Experssion
{
    public abstract class Expr
    {
        public abstract T accepte<T>(Expr.IVistor<T> vistor);

        public interface IVistor<T>
        {
            T visitGroupingExpr(Grouping t);
            T visitLiteralExpr(Literal t);
            T visitBinaryExpr(Binary t);
            T visitUnaryExpr(Unary t);
            T visitTernaryExpr(Ternary t);
            T visitVariableExpr(Variable t);
            T visitAssignExpr(Assign t);
            T visitLogicalExpr(Logical  t);
        }

    }
}
