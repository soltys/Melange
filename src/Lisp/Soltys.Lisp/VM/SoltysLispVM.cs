using System;
using System.Collections.Generic;
using Soltys.Library.TextAnalysis;
using Soltys.Lisp.Compiler;
using Soltys.Lisp.CoreLib;
using Soltys.VirtualMachine;

namespace Soltys.Lisp
{
    public class SoltysLispVM : IDisposable
    {
        private class VMFacade
        {
            private readonly VM vm;

            public VMFacade()
            {
                this.vm = new VM();
            }

            public void LoadCoreLibrary()
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

        public SoltysLispVM()
        {
            this.env = new LispEnv();
            this.vmFacade = new VMFacade();
        }

        public void Initialize()
        {
            this.vmFacade.LoadCoreLibrary();
            this.env.VisitLibrary(new CoreLibrary());
        }

        public object Do(string input)
        {
            var astNodes = GetProgramAst(input);
            object? lastValue = null;
            foreach (var ast in astNodes)
            {
                var functions = CodeGeneration(ast);
                lastValue = this.vmFacade.Run(functions);
            }
            return lastValue ?? "nil";
        }

        private static IEnumerable<IAstNode> GetProgramAst(string input)
        {
            var lexer = new LispLexer(new TextSource(input));
            var parser = new LispParser(new TokenSource<LispToken, LispTokenKind>(lexer));
            return parser.ParseProgram();
        }

        private IEnumerable<VMFunction> CodeGeneration(IAstNode ast)
        {
            var modifiedAst = this.env.Eval(ast);
            var evalVisitor = new CodeGenVisitor(this.env.Copy());
            modifiedAst.Accept(evalVisitor);
            return evalVisitor.Functions;
        }

        public void Dispose()
        {
        }
    }
}
