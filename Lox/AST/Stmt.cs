using System;
using System.Collections.Generic;
using System.Text;

namespace Lox.AST
{

    // vistor patren
    public abstract  class Stmt
    {
        public abstract T Accept<T>(IVistor<T> visitor);

        public interface  IVistor<T>
        {

            T visit(T t);
        }
    }
}
