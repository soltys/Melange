using System;
using Mono.Terminal;

namespace Soltys.Lisp
{
    internal class Repl
    {
        private readonly LineEditor lineEdit;

        public Repl()
        {
            this.lineEdit = new LineEditor("slisp");
        }

        public void Run()
        {
            PrintWelcome();
            using var slisp = new SoltysLisp();
            slisp.Initialize();
            while (true)
            {
                var input = this.lineEdit.Edit("slisp> ", "");
                if (input.Trim() == "!exit")
                {
                    break;
                }

                var result = slisp.Do(input);
                Console.Write($"<-- {result}");
                Console.WriteLine();
            }
        }

        private void PrintWelcome()
        {
            Console.WriteLine("+================================+");
            Console.WriteLine("| Welcome to Soltys.Lisp (slisp) |");
            Console.WriteLine("+================================+");
            Console.WriteLine("To exit enter !exit");
            Console.WriteLine();
        }
    }
}
