using System;
using System.Collections.Generic;
using System.Text;

namespace Lox
{
    class RuntimeException : Exception
    {
        public Token token  { get; set; }
        public RuntimeException(Token  token, string  message) :base(message)
        {
            this.token = token;
        }
    }
}
