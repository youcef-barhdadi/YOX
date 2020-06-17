using System;
using System.Collections.Generic;
using System.Text;

namespace Lox
{
    class Token
    {
        TokenType type;
        string lexeme;
        object literal;
        int line;
        private object eOF;
        private string v;
        private object p;

        public Token(object eOF, string v, object p, int line)
        {
            this.eOF = eOF;
            this.v = v;
            this.p = p;
            this.line = line;
        }

        Token(TokenType type, String lexeme, Object literal, int line)
        {
            this.type = type;
            this.lexeme = lexeme;
            this.literal = literal;
            this.line = line;
        }

        public override String ToString()
        {
            return type + " " + lexeme + " " + literal;
        }




   

    }
}
