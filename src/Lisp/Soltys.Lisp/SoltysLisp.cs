using System;
using System.Collections.Generic;
using Soltys.Library.TextAnalysis;
using Soltys.Lisp.Compiler;
using Soltys.VirtualMachine;

namespace Soltys.Lisp
{
    public class SoltysLisp : IDisposable
    {
        private class VMFacade
        {
            private VM vm;

            public VMFacade()
            {
                this.vm = new VM();
            }

            public void Initialize()
            {
                var initInstructions = new IInstruction[] {
                    //Load itself into VM
                    new LoadLibraryInstruction("Soltys.Lisp"),
                };
                this.vm.Load(new[] { new VMFunction("Main", initInstructions), });
                this.vm.Run();
                this.vm.Clear();
            }

            public object? Run(IEnumerable<VMFunction> vmFunctions)
            {
                this.vm.Load(vmFunctions);
                this.vm.Run();
                var value = this.vm.PeekValueStack;
                this.vm.Clear();
                return value;
            }
        }

        private readonly LispEnv env;
        private readonly VMFacade vmFacade;


        public SoltysLisp()
        {
            this.env = new LispEnv();
            this.vmFacade = new VMFacade();
        }

        public void Initialize()
        {
            this.vmFacade.Initialize();
        }

        public object Do(string input)
        {
            var ast = GetAst(input);

            ast = this.env.Eval(ast);
            var functions = CodeGeneration(ast);
            return this.vmFacade.Run(functions) ?? "nil";
        }

        private static IEnumerable<IAstNode> GetAst(string input)
        {
            var lexer = new LispLexer(new TextSource(input));
            var parser = new LispParser(new TokenSource<LispToken, LispTokenKind>(lexer));
            return parser.ParseProgram();
        }

        private IEnumerable<VMFunction> CodeGeneration(IEnumerable<IAstNode> ast)
        {
            var evalVisitor = new CodeGenVisitor();
            foreach (var astNode in ast)
            {
                astNode.Accept(evalVisitor);
            }

            return evalVisitor.Functions;

        }

        public void Dispose()
        {
        }
    }
}
