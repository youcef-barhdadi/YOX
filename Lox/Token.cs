using System;
using System.Collections.Generic;
using System.Text;

namespace Lox
{
  public  class Token
    {
        public TokenType type { get; set; }
       public string lexeme { get; set; }
        public object Literal { get; set; }
       public int Line { get; set; }
        private object EOF { get; set; }
        private string v;
        private object p;


       public Token(TokenType type, String lexeme, Object literal, int line)
        {
            this.type = type;
            this.Lexeme = lexeme;
            this.Literal = literal;
            this.Line = line;
        }

        public TokenType Type { get => type; set => type = value; }
        public string Lexeme { get => lexeme; set => lexeme = value; }

        public override String ToString()
        {
            return type + " " + Lexeme + " " + Literal;
        }




   

    }
}
