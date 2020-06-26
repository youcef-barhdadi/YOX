using System;
using System.Collections.Generic;
using System.Text;
using Lox.AST;
using Lox.Experssion;


namespace Lox
{
    class Interpreter : Stmt.IVistor<object>
    {
        public object visitBinaryExpr(Binary t)
        {
            object left = evaluate(t.Left);
            object right = evaluate(t.Right);


            switch(t.Operator.Type)
            {
                case TokenType.PLUS:
                 if (left is double && right is double)
                        return (double)left + (double)right;
                    if (left is string && right is string)
                        return (string)left + (string)right;
                    break;
                case TokenType.MINUS:
                    return (double)left - (double)right;
                case TokenType.STAR:
                    return (double)left * (double)right;
                case TokenType.SLASH:
                    return (double)left / (double)right;

                // logic operator

                case TokenType.GREATER:
                    return (double)left > (double)right;
                case TokenType.GREATER_EQUAL:
                    return (double)left >= (double)right;
                case TokenType.LESS:
                    return (double)left < (double)right;
                case TokenType.LESS_EQUAL:
                    return (double)left <= (double)right;
                case TokenType.EQUAL_EQUAL:
                    return isEqual(left, right);
                case TokenType.BANG_EQUAL:
                    return !isEqual(left, right);

            }
            return null;

        }

        private bool isEqual(object left, object right)
        {
            if (left == null && right == null)
                return true;
            if (left == null)
                return false;
            return left.Equals(right);
        }

        public object visitGroupingExpr(Grouping t)
        {
            return evaluate(t.Experssion);
        }

        private object evaluate(Expr experssion)
        {
            return experssion.accepte(this);
        }

        public object visitLiteralExpr(Literal t)
        {
            return t.Value;
        }

        public object visitTernaryExpr(Ternary t)
        {
            throw new NotImplementedException();
        }

        public object visitUnaryExpr(Unary t)
        {
            object right = evaluate(t.Right);

            switch(t.Operator.Type)
            {
                case TokenType.MINUS:
                    return -(Double)right;
                case TokenType.BANG:
                    return !isTruthy(right);

            }
            return null;
        }

        private bool isTruthy(object obj)
        {
            if (obj == null)
                return false;
            if (obj is bool)
                return (bool)obj;
            return false;
        }
    }
}
