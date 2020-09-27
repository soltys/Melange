using System;
using System.Collections.Generic;
using Soltys.Lisp.Compiler;
using Soltys.VirtualMachine;

namespace Soltys.Lisp
{
    internal class LispEnv
    {
        private class EvalVisitor : IAstVisitor
        {
            private readonly List<IInstruction> instructions;
            public IEnumerable<IInstruction> Instructions => this.instructions;
            public EvalVisitor()
            {
                this.instructions = new List<IInstruction>();
            }

            private void Visit(IAstNode node) => node.Accept(this);
            private void AddInstr(IInstruction instruction) => this.instructions.Add(instruction);

            public void VisitList(AstList ast)
            {
                if (ast.Elements.Count == 0)
                {
                    return;
                }
                
                for (var i = 1; i < ast.Elements.Count; i++)
                {
                    Visit(ast[i]);
                }

                Visit(ast[0]);
            }

            public void VisitNumber(AstNumber ast)
            {
                switch (ast)
                {
                    case AstIntNumber i:
                        AddInstr(new LoadConstantInstruction(LoadKind.Integer, i.Value));
                        break;
                    case AstDoubleNumber d:
                        AddInstr(new LoadConstantInstruction(LoadKind.Double, d.Value));
                        break;
                }
            }

            public void VisitSymbol(AstSymbol ast)
            {
                switch (ast.Name)
                {
                    case "add":
                    case "+":
                        AddInstr(new AddInstruction());
                        break;
                    case "sub":
                    case "-":
                        AddInstr(new SubtractionInstruction());
                        break;
                    case "mul":
                    case "*":
                        AddInstr(new MultiplicationInstruction());
                        break;
                    case "div":
                    case "/":
                        AddInstr(new DivisionInstruction());
                        break;
                    default:
                        throw new InvalidOperationException($"Symbol named {ast.Name} was not expected");
                }
            }

            public void VisitString(AstString ast) => 
                AddInstr(new LoadStringInstruction(ast.Value));
        }

        private readonly VM vm;
        public LispEnv()
        {
            this.vm = new VM();
        }

        public object Eval(IEnumerable<IAstNode> ast)
        {
            var evalVisitor = new EvalVisitor();
            foreach (var astNode in ast)
            {
                Eval(astNode, evalVisitor);
            }

            this.vm.Load(evalVisitor.Instructions);
            this.vm.Run();
            return this.vm.PeekValueStack;
        }

        private static void Eval(IAstNode ast, IAstVisitor evalVisitor) =>
            ast.Accept(evalVisitor);
    }
}
