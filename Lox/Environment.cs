using System;
using System.Collections.Generic;
using System.Text;

namespace Lox
{
    public class Environment
    {
        private Dictionary<string, object> map = new Dictionary<string, object>();
        Environment enclosing;

        public Environment()
        {
            enclosing = null;
        }


        public Environment(Environment env)
        {
            enclosing = env;
        }

        //public  object this[index]
        //{
        //   { return map[Index]; }
        //}

        public void Define(string name, object  value)
        {
             //map.Add(name, value);


           map[name] = value;
        }

        public void assign(Token name, object value)
        {
           if (map.ContainsKey(name.lexeme))
            {
                map[name.lexeme] = value;
                return; 
            }
           if (enclosing != null)
            {
                enclosing.assign(name, value);
                return;
            }    
            throw new RuntimeException(name, $"Undefined variable '{name.lexeme}'");
        }



        public object Get(Token name)
        {
            if (map.ContainsKey(name.lexeme))
                return map[name.lexeme];
            if (enclosing != null)
                return enclosing.Get(name);
            throw new RuntimeException(name,  $"Undefined variable  ${name.lexeme} .");
        }

    }
}
