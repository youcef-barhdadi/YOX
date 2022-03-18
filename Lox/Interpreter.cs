using System;
using System.Collections.Generic;
using System.Text;
using Lox.AST;
using Lox.Experssion;
using Lox.Stmts;

namespace Lox
{
    class Interpreter : Expr.IVistor<object> , Stmts.Stmt.Visitor<object>
    {


        private Environment env = new Environment();

        public void interpret(List<Stmt> statements)
        {
            try
            {
                foreach (Stmt item in statements)
                {
                    execute(item);
                }
                    
            }
            catch (RuntimeException  e)
            {

                Program.runtimeError(e);
            }
            catch 
            {
                System.Environment.Exit(5);

            }
        }

        private void execute(Stmt item)
        {
            item.accept(this);
        }

        private string stringify(object value)
        {
            if (value == null)
                return "nil";

            if (value is double)
            {
                string text = value.ToString();

                if (text.EndsWith(".0"))
                {
                    text = text.Substring(0, text.Length - 2);
                }
                return text;
            }
            return value.ToString();
         }

        public object visitBinaryExpr(Binary t)
        {
            object left = evaluate(t.Left);
            object right = evaluate(t.Right);


            switch (t.Operator.Type)
            {
                case TokenType.PLUS:
                    if (left is double && right is double)
                    {
                        return (double)left + (double)right;
                    }
                    if (left is string && right is string)
                    {
                        return (string)left + (string)right;
                    }
                    if (left is string  || right is string)
                    {
                        return left.ToString() + right.ToString();
                    }
                    throw new RuntimeException(t.Operator, "Operands must be two numbers or two strings.");
                case TokenType.MINUS:
                    checkNumberOperands(t.Operator, left, right);
                    return (double)left - (double)right;
                case TokenType.STAR:
                    checkNumberOperands(t.Operator, left, right);
                    return (double)left * (double)right;
                case TokenType.SLASH:
                    checkNumberOperands(t.Operator, left, right);
                    if ((double)right == 0)
                        throw new RuntimeException(t.Operator, "Can't devid by zero!");
                    return (double)left / (double)right;

                // logic operator

                case TokenType.GREATER:
                    checkNumberOperands(t.Operator, left, right);
                    return (double)left > (double)right;
                case TokenType.GREATER_EQUAL:
                    checkNumberOperands(t.Operator, left, right);
                    return (double)left >= (double)right;
                case TokenType.LESS:
                    checkNumberOperands(t.Operator, left, right);
                    return (double)left < (double)right;
                case TokenType.LESS_EQUAL:
                    checkNumberOperands(t.Operator, left, right);
                    return (double)left <= (double)right;
                case TokenType.EQUAL_EQUAL:
                            return isEqual(left, right);
                case TokenType.BANG_EQUAL:
                   // checkNumberOperands(t.Operator, left, right);
                    return !isEqual(left, right);

            }
     
     
            return null;

        }

        private void checkNumberOperands(Token @operator, object right, object left)
        {

            if (left is double && right is double)
                return;

            throw new RuntimeException(@operator, "Operands must be numbers.");
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

        public object visitLogicalExpr(Logical exper)
        {
            object left = evaluate(exper.Left);

            if (exper.Operator.type == TokenType.OR)
            {
                // we need just to check just left if it  is true leave
               if (isTruthy(left))
                    return left;
            }else
            {
                // in the case of and if the left is false just leave
                if (!isTruthy(left))
                    return left;
            }

            return evaluate(exper.Right);
        }

        public object visitTernaryExpr(Ternary t)
        {
            object predcate = evaluate(t.Predcate);

            if (isTruthy(predcate))
                    return evaluate(t.expre1);
            return evaluate(t.expre2);
        }

        public object visitUnaryExpr(Unary t)
        {
            object right = evaluate(t.Right);

            switch (t.Operator.Type)
            {
                case TokenType.MINUS:
                    checkNumberOperand(t.Operator, right);
                    return -(Double)right;
                case TokenType.BANG:
                    return !isTruthy(right);

            }
            return null;
        }

        private void checkNumberOperand(Token @operator, object operand)
        {

            if (operand is double)
                return;
            throw new RuntimeException(@operator, "Operand must be a number");

        }

        private bool isTruthy(object obj)
        {
            if (obj == null)
                return false;
            if (obj is bool)
                return (bool)obj;
            if (obj is TokenType)
                return (TokenType)obj == TokenType.TRUE;

            return false;
        }

     
        object Stmt.Visitor<object>.visitPrintStmt(Print stmt)
        {

           object value =  evaluate(stmt.Expression);
           Console.WriteLine(stringify(value));
            return null;
        }

        object Stmt.Visitor<object>.visitExpressionStmt(Expression stmt)
        {

            evaluate(stmt.expression);

            return null;
        }

        public object visitVariableExpr(Variable t)
        {
            return env.Get(t.Name);
        }

        public object visitVarStmt(Var stmt)
        {
            object value = null;

            if (stmt.Initializer != null)
            {
                value = evaluate(stmt.Initializer);
            }

            env.Define(stmt.Name.lexeme, value);
            return null;
        }
        
        public  object visitWhileStmt(While stmt)
        {
            while (isTruthy(evaluate(stmt.Condition)))
            {
                execute(stmt.Body);
            }
            return null;
        }



        public object visitBlockStmt(Block  block)
        {
            executeBlock(block.statements,   new Environment(env));
            return null;
        }

        private void executeBlock(List<Stmt> statements, Environment environment)
        {
            Environment previous = this.env;
            try
            {
                this.env = environment;
                foreach (var item in statements)
                {
                    execute(item);
                }
            }
            finally
            {
                this.env = previous;
            }
        }

        public object visitAssignExpr(Assign t)
        {
            object value = evaluate(t.Value);
      //      env.Define(t.Name.lexeme, value);
            env.assign(t.Name, value);
            return null;
        }

        object Stmt.Visitor<object>.visitIfStmt(If stmt)
        {
            if (isTruthy(evaluate(stmt.condation)))
            {
                execute(stmt.thenBranch);
            }
            else if (stmt.elseBranch != null)
            {
                execute(stmt.elseBranch);
            }

            return null;
        }

        //public object visitWhileStmt(While stmt)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
