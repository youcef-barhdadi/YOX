using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lox
{
    class Scanner
    {
        private string source;
        private  List<Token> tokens = new List<Token>();
        private int start = 0;
        private int current = 0;
        private int line = 1;
        Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>();

        public Scanner(string source)
        {
            this.source = source;
         
            keywords.Add("and",TokenType.AND);
            keywords.Add("class", TokenType.CLASS);
            keywords.Add("else", TokenType.ELSE);
            keywords.Add("false", TokenType.FALSE);
            keywords.Add("for", TokenType.FOR);
            keywords.Add("fun", TokenType.FUN);
            keywords.Add("if", TokenType.IF);
            keywords.Add("nil", TokenType.NIL);
            keywords.Add("or", TokenType.OR);
            keywords.Add("print", TokenType.PRINT);
            keywords.Add("return", TokenType.RETURN);
            keywords.Add("super", TokenType.SUPER);
            keywords.Add("this", TokenType.THIS);
            keywords.Add("true", TokenType.TRUE);
            keywords.Add("var", TokenType.VAR);
            keywords.Add("while", TokenType.WHILE);
        }

       
       public List<Token> scanTokens()
        {
            while (!isAtEnd())
            {
                start = current;
                scanToken();
            }
         
            tokens.Add(new Token(TokenType.EOF, "", null, line));
            return tokens;
        }

        private void scanToken()
        {
            char c = advance();
            switch (c)
            {
                case '(': addToken(TokenType.LEFT_PAREN); break;
                case ')': addToken(TokenType.RIGHT_PAREN); break;
                case '?': addToken(TokenType.QUTION); break;
                case ':': addToken(TokenType.DDOT); break;
                case '{': addToken(TokenType.LEFT_BRACE); break;
                case '}': addToken(TokenType.RIGHT_BRACE); break;
                case ',': addToken(TokenType.COMMA); break;
                case '.': addToken(TokenType.DOT); break;
                case '-': addToken(TokenType.MINUS); break;
                case '+': addToken(TokenType.PLUS); break;
                case ';': addToken(TokenType.SEMICOLON); break;
                case '*': addToken(TokenType.STAR); break;
                case '!': addToken(match('=') ? TokenType.BANG_EQUAL : TokenType.BANG); break;
                case '=': addToken(match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL); break;
                case '<': addToken(match('=') ? TokenType.LESS_EQUAL : TokenType.LESS); break;
                case '>': addToken(match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER); break;
                case '/':
                    if (match('/'))
                    {
                        while (peek() != '\n' && !isAtEnd()) advance();
                    }
                    else if (match('*'))
                    {
                        /**/
                        while ((peek() != '*' && peekNext() != '/'))
                        {
                            if (peek() == '\n')
                                line++;
                            advance();
                        }
                    }
                    else
                        addToken(TokenType.SLASH);


                    break;

                case ' ':
                case '\r':
                case '\t':
                    // Ignore whitespace.
                    break;

                case '\n':
                    line++;
                    break;
                case '"': read_string(); break;

                default:
                    if (char.IsDigit(c))
                        number();
                    else if (isAlpha(c))
                        identifier();
                    else
                        Program.error(line, "Unexpected character.");
                break;
            }
        }

        private void identifier()
        {
            while (isAlphaNumeric(peek())) advance();
            string text = source.Substring(start, current - start);

            TokenType type = TokenType.IDENTIFIER;
            bool exist =    keywords.TryGetValue(text, out type);
            addToken(type);
        }
       
        private bool isDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private void number()
        {
            while (isDigit(peek())) advance();

            if (peek() == '.' && char.IsDigit(peekNext()))
            {
                advance();

                while (char.IsDigit(peek())) advance();
            }

            addToken(TokenType.NUMBER,
                Double.Parse(source.Substring(start, (current   - start) )));
        }

        private char peekNext()
        {
            if (current + 1 >= source.Length) return '\0';
            return source[current + 1];
        }

        private void read_string()
        {
            while (peek() != '"' && !isAtEnd())
            {
                if (peek() == '\n')
                    line++;
                advance();
            }

            if (isAtEnd())
            {
                Program.error(line, "Unterminated string.");
            }
            advance();

            string value = source.Substring(start + 1,    current - 1  - (start +1));
            addToken(TokenType.STRING, value);


        }

        private char peek()
        {
            if (isAtEnd())
                return '\0';
            return source[current];
        }

        private bool match(char expected)
        {
            if (isAtEnd())
                return false;
            if (source[current] != expected)
                return false;
            current++;
            return true;
        }

        private void addToken(TokenType type)
        {
            addToken(type, null);
        }

        private void addToken(TokenType type, Object literal)
        {
            String text = source.Substring(start,    current - start);
            tokens.Add(new Token(type, text, literal, line));
        }



        private char advance()
        {
            current++;
            return source[current - 1];
        }

        private bool isAtEnd()
        {
            return  current >= source.Length ;
        }



        private bool isAlphaNumeric(char c)
        {
            return isAlpha(c) || char.IsDigit(c);
        }
        private bool isAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') ||
                   (c >= 'A' && c <= 'Z') ||
                    c == '_';
        }
    }
}
