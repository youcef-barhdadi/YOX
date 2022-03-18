using System;
using System.Collections.Generic;
using System.Text;
using Lox.Experssion;

namespace Lox.AST
{
    public class RPN : Expr.IVistor<string>
    {
        public string print(Expr expr)
        {
            return expr.accepte(this);
        }
        string Expr.IVistor<string>.visitGroupingExpr(Grouping t)
        {
            return parenthesize("group", t.Experssion);
        }


        public string parenthesize(string name, params Expr[] exper)
        {
            StringBuilder builder = new StringBuilder();
            foreach (Expr e in exper)
            {
                builder.Append(e.accepte(this));
                builder.Append(" ");

            }
            builder.Append(name).Append(" ");
            return builder.ToString();
        }

        public string visitLiteralExpr(Literal t)
        {
            if (t.Value == null)
                return "nil";
            return t.Value.ToString();
        }

        public string visitBinaryExpr(Binary t)
        {
            return parenthesize(t.Operator.Lexeme, t.Left, t.Right);
        }

        public string visitUnaryExpr(Unary t)
        {
            return parenthesize(t.Operator.Lexeme, t.Right);
        }

        public string visitTernaryExpr(Ternary t)
        {
            return parenthesize("rny", t.Predcate, t.expre1, t.expre2);



        }

        public string visitVariableExpr(Variable t)
        {
            throw new NotImplementedException();
        }

        public string visitAssignExpr(Assign t)
        {
            throw new NotImplementedException();
        }

       public string visitLogicalExpr(Logical t)
        {
            throw new NotImplementedException();
        }



    }
}
