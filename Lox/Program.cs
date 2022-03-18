using System;
using System.Collections.Generic;
using System.IO;
using Lox.AST;
using Lox.Experssion;

namespace Lox
{
    class Program
    {
       static Interpreter inter = new Interpreter();
        static bool hadError = false;
        private static bool hadRuntimeError = false;

        static void Main(string[] args)
        {
            //Console.WriteLine(System.Environment.CurrentDirectory);
            if (args.Length > 1)
                Console.WriteLine("Usage: jlox [script]");
            //else if (args.Length == 1)
            //    runFile(args[0]);
            //else

            //while(true)
            //    runPromp();
            runFile("../../../test/first.lox");

            //Expr expression = new Binary(new Unary(new Literal(1337),
            //                            new Token(TokenType.MINUS, "-", null, 1)),
            //                            new Token(TokenType.PLUS, "+", null, 1), new Literal(6));

            //Console.WriteLine(new RPN().print(expression)); ;

        }

        private static void runPromp()
        {
            while (true)
            {
                string s = Console.ReadLine();
                run(s);
                hadError = false;
                hadRuntimeError = false;
            }
        }

        private static void runFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File dosn't exist");
                return ;
            }

            string text = File.ReadAllText(filename);

            run(text);
            if (hadError)
              System.Environment.Exit(1);
            if (hadRuntimeError)
                System.Environment.Exit(1);

        }

        private static void run(string source)
        {
            Scanner scanner = new Scanner(source);
            List<Token> tokens = scanner.scanTokens();
            Parser p = new Parser(tokens);

            List<Stmts.Stmt> expression = p.Parse();

            if (expression == null)
                return;
            if (hadRuntimeError)
                return;
            inter.interpret(expression);

            // Console.WriteLine(new AstPrinter().print(expression));
        }

        public static void error(Token token, String message)
        {
            if (token.type == TokenType.EOF)
            {
                report(token.Line, " at end", message);
            }
            else
            {
                report(token.Line, " at '" + token.lexeme + "'", message);
            }
        }


        public static void error(int line, String message)
        {

            report(line, "", message);
        }



      public  static void runtimeError(RuntimeException error)
        {
            Console.WriteLine (error.Message +
                "\n[line " + error.token.Line + "]");
            hadRuntimeError = true;
        }
        private static void report(int line, String where, String message)
        {
            Console.WriteLine(
                "[line " + line + "] Error" + where + ": " + message);
            hadError = true;
        }
    }
}
