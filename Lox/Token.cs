using System;
using System.Collections.Generic;
using System.Text;

namespace Lox
{
  public  class Token
    {
        TokenType type;
        string lexeme;
        object literal;
        int line;
        private object eOF;
        private string v;
        private object p;


       public Token(TokenType type, String lexeme, Object literal, int line)
        {
            this.type = type;
            this.Lexeme = lexeme;
            this.literal = literal;
            this.line = line;
        }

        public TokenType Type { get => type; set => type = value; }
        public string Lexeme { get => lexeme; set => lexeme = value; }

        public override String ToString()
        {
            return type + " " + Lexeme + " " + literal;
        }




   

    }
}
