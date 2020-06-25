﻿using System;
using System.Collections.Generic;
using System.Text;
using Lox.AST;
using Lox.Experssion;

namespace Lox
{


    public class ParsserExpetion : Exception
    {
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
            unary          → ( "!" | "-" ) unary
                           | primary ;
            primary        → NUMBER | STRING | "false" | "true" | "nil"
                           | "(" expression ")" ;
             
             */


        public Expr Parse()
        {
            try
            {
                return expression();
            }
            catch (ParsserExpetion error)
            {
                return null;
            }
        }

        private Expr expression()
        {
            return equality();
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

            if (match(TokenType.LEFT_PAREN))
            {
                Expr e = expression();
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
