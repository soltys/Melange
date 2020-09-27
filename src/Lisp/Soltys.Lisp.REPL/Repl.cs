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

            while (true)
            {
                var input = this.lineEdit.Edit("slisp> ", "");
                if (input == "!exit")
                {
                    break;
                }

                Console.Write("<-- ");
                Console.WriteLine(slisp.Do(input));
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
