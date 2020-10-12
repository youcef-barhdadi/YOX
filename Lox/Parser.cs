using System;
using System.Collections.Generic;
using System.Text;
using Lox.AST;
using Lox.Experssion;
using Lox.Stmts;

namespace Lox
{


    public class ParsserExpetion : Exception
    {


        private Dictionary<string, object> map = new Dictionary<string, object>();

        public ParsserExpetion()
        {

        }
        public ParsserExpetion(string? message) : base(message)
        {

        }
    }
    public class Parser
    {
        public List<Token> Tokens { get; }

        public int Current { get; set; } = 0;


        public Parser(List<Token> tokens)
        {
            this.Tokens = tokens;
        }




        /*
         
            expression     → equality ;
            equality       → comparison ( ( "!=" | "==" ) comparison )* ;
            comparison     → addition ( ( ">" | ">=" | "<" | "<=" ) addition )* ;
            addition       → multiplication ( ( "-" | "+" ) multiplication )* ;
            multiplication → unary ( ( "/" | "*" ) unary )* ;
            unary          → ( "!" | "-" ) unary | primary ;
            primary        → NUMBER | STRING | "false" | "true" | "nil"
                           | "(" expression ")" ;
             
             */


        public List<Stmt> Parse()
        {
            List<Stmt> statements = new List<Stmt>();
            while (!isAtEnd())
            {
                statements.Add(declaration());
            }

            return statements;
        }


        private Stmt declaration()
        {
            try
            {
                if (match(TokenType.VAR))
                    return varDeclaration();
                return statement();
            }
            catch
            {
                synchronize();
                return null;
            }
        }

        private  Stmt varDeclaration()
        {
            Token token = consume(TokenType.IDENTIFIER, "Expect variable name.");
            Expr initi = null;
            if (match(TokenType.EQUAL))
            {
                initi = Tnary();
            }
            consume(TokenType.SEMICOLON, "Expect ';' after variable declaration.");
            return new Var(token, initi);
        }
        private Stmt statement()
        {
            if (match(TokenType.IF))
                return (ifStatement());
            if (match(TokenType.PRINT))
                return printStatement();
            if (match(TokenType.LEFT_BRACE))
                return new Block(block());
            return expressionStatement();
        }
        public Stmt ifStatement()
        {
            consume(TokenType.LEFT_PAREN, "Expect '(' after 'if'.");
            Expr condation = expression();
            consume(TokenType.RIGHT_PAREN, "Expect ')' after if condition.");

            Stmt thenbranch = statement();
            Stmt elsebranch = null;
            if (check(TokenType.ELSE))
            {
                elsebranch = statement();
            }
            return new If(condation, thenbranch, elsebranch) ;
        }

        private List<Stmt> block()
        {
            List<Stmt> statements = new List<Stmt>();

            while(!check(TokenType.RIGHT_BRACE) &&  !isAtEnd())
            {
                statements.Add(declaration());
            }
            consume(TokenType.RIGHT_BRACE, "Expect '}' after block.");

            return statements;
        }


        private Stmt expressionStatement()
        {
            Expr value = Tnary();
            consume(TokenType.SEMICOLON, "Expect ';' after value.");
            return new Expression(value);

        }

        private Stmt printStatement()
        {
            Expr value = Tnary();
            consume(TokenType.SEMICOLON, "Expect ';' after value.");
            return new Print(value);
        }

        private  Expr Tnary()
        {
            Expr e = expression();

            if (match(TokenType.QUTION))
            {
                Expr exp = expression();
                Expr e2 = null;
                if (match(TokenType.DDOT))
                {
                    e2 = expression();
                }
                else
                    return null;

                return new Ternary(e, exp, e2);
            }

            return e;
        }
        private Expr expression()
        {
            return assignment();
        }
        
        private  Expr assignment()
        {
            Expr ex = equality();


            if (match(TokenType.EQUAL))
            {
                Token equle = previous();

                // check me if somting happen 
                Expr value = Tnary();
                if (ex is Variable)
                {
                    Token name = ((Variable)ex).Name;
                    return new Assign(name, value);
                }
                error(equle, "Invalid assignment target.");


            }

            return ex;
        }


        private Expr equality()
        {
            Expr exper = comparison();

            while (match(TokenType.BANG_EQUAL, TokenType.EQUAL_EQUAL))
            {
                Token opertor = previous();
                Expr right = comparison();
                exper = new Binary(exper, opertor, right);
            }
            return exper;
        }
        private Expr comparison()
        {
            Expr exper = addition();
            while (match(TokenType.GREATER, TokenType.GREATER_EQUAL, TokenType.LESS_EQUAL, TokenType.LESS))
            {
                Token opr = previous();
                Expr right = addition();
                exper = new Binary(exper, opr, right);
            }
            return exper;
        }

        private Expr addition()
        {
            Expr exper = multiplication();
            while (match(TokenType.PLUS, TokenType.MINUS))
            {
                Token opr = previous();
                Expr right = multiplication();
                exper = new Binary(exper, opr, right);

            }
            return exper;
        }

        private Expr multiplication()
        {
            Expr exper = unary();
            while (match(TokenType.STAR, TokenType.SLASH))
            {
                Token opr = previous();
                Expr right = unary();
                exper = new Binary(exper, opr, right);
            }
            return exper;
        }

        private Expr unary()
        {
            if (match(TokenType.MINUS, TokenType.BANG))
            {
                Token opr = previous();
                Expr ex = unary();
              return  new Unary(ex, opr);
            }
            return primary();
        }

        private Expr primary()
        {
            // CHeck me
            if (match(TokenType.EOF))
                return null;
            if (match(TokenType.FALSE))
                return new Literal(TokenType.FALSE);
            if (match(TokenType.TRUE))
                return new Literal(TokenType.TRUE);
            if (match(TokenType.NIL))
                return new Literal(TokenType.NIL);
            // not sure , check me after
            if (match(TokenType.NUMBER, TokenType.STRING))
                return new Literal(previous().Literal);
            if (match(TokenType.IDENTIFIER))
                return new Variable(previous());

            if (match(TokenType.LEFT_PAREN))
            {
                Expr e = Tnary();
                consume(TokenType.RIGHT_PAREN, "Expect ')' after expression.");
                return new Grouping(e);
            }

            //  if we dont find literlel or expression
            throw error(peek(), "Expect expression!.");
        }


        private ParsserExpetion error(Token token, String message)
        {
            Program.error(token, message);
            return new ParsserExpetion();
        }


        public void synchronize()
        {
            advance();

            while (isAtEnd())
            {
                if (previous().type == TokenType.SEMICOLON)
                    return;

                switch (peek().type)
                {
                    case TokenType.CLASS:
                    case TokenType.FUN:
                    case TokenType.VAR:
                    case TokenType.FOR:
                    case TokenType.IF:
                    case TokenType.WHILE:
                    case TokenType.PRINT:
                    case TokenType.RETURN:
                        return;
                }
                advance();
            }
        }


        private Token consume(TokenType type, string v)
        {
            if (check(type))
                return advance();
            else
                throw error(peek(), v);

        }

        private Token previous()
        {
            if (Current != 0)
                return this.Tokens[Current - 1];
            return null;
        }

        private bool match(params TokenType[] list)
        {
            foreach (TokenType item in list)
            {
                if (check(item))
                {
                    advance();
                    return true;
                }
            }
            return false;
        }

        private Token advance()
        {
            if (!isAtEnd())
                Current++;
            return previous();
        }

        private bool check(TokenType type)
        {
            if (isAtEnd())
                return false;
            return peek().Type == type;

        }

        private bool isAtEnd()
        {
            return this.peek().Type == TokenType.EOF;
        }

        private Token peek()
        {
            return this.Tokens[Current];
        }



    }
}
