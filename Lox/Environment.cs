using System;
using System.Collections.Generic;
using System.Text;

namespace Lox
{
    public class Environment
    {
        private Dictionary<string, object> map = new Dictionary<string, object>();




        //public  object this[index]
        //{
        //   { return map[Index]; }
        //}

        public void Define(string name, object  value)
        {
             //map.Add(name, value);


           map[name] = value;
        }

        public object Get(Token name)
        {
            if (map.ContainsKey(name.lexeme))
                return map[name.lexeme];
            throw new RuntimeException(name,  $"Undefined variable  ${name.lexeme} .");
        }

    }
}
