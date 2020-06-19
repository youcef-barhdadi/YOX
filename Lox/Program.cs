using System;
using System.Collections.Generic;
using System.IO;
using Lox.AST;
using Lox.Experssion;

namespace Lox
{
    class Program
    {

        static bool hadError = false;

        static void Main(string[] args)
        {
            //    if (args.Length > 1)
            //        Console.WriteLine("Usage: jlox [script]");
            //    else if (args.Length == 1)
            //        runFile(args[0]);
            //    else
            //        runPromp();

            Expr expression = new Binary(new Unary(new Literal(1337),
                                        new Token(TokenType.MINUS, "-",null, 1)),
                                        new Token(TokenType.PLUS, "+",null, 1), new Literal(6));

            Console.WriteLine(new AstPrinter().print(expression)); ;

        }

        private static void runPromp()
        {
            while (true)
            {
                string s = Console.ReadLine();
                run(s);
                hadError = false;
            }
        }

        private static void runFile(string filename)
        {
            string text = File.ReadAllText(filename);

            run(text);
            if (hadError)
                Environment.Exit(1);

        }

        private static void run(string source)
        {
            Scanner scanner = new Scanner(source);
            List<Token> tokens = scanner.scanTokens();


            foreach (Token t in tokens)
            {
                Console.WriteLine(t);
            }
        }



        public static void error(int line, String message)
        {
            report(line, "", message);
        }

        private static void report(int line, String where, String message)
        {
            Console.WriteLine(
                "[line " + line + "] Error" + where + ": " + message);
            hadError = true;
        }
    }
}
