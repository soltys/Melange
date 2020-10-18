using System;
using System.Collections.Generic;
using System.Linq;
using Soltys.VirtualMachine;

namespace Soltys.Lisp.Compiler
{
    internal class CodeGenVisitor : IAstVisitor
    {
        private readonly List<IInstruction> instructions;
        private readonly Dictionary<string, VMFunction> userDefinedFunctions = new Dictionary<string, VMFunction>();
        private IReadOnlyEnvData readOnlyEnvData;

        public IEnumerable<VMFunction> Functions => this.userDefinedFunctions.Values.Concat(new[] {
            new VMFunction("Main", this.instructions)
        });
        public CodeGenVisitor(IReadOnlyEnvData readOnlyEnvData)
        {
            this.instructions = new List<IInstruction>();
            this.readOnlyEnvData = readOnlyEnvData ?? throw new ArgumentNullException(nameof(readOnlyEnvData));
        }

        private void Visit(IAstNode node) => node.Accept(this);
        private void AddInstr(IInstruction instruction) => this.instructions.Add(instruction);

        void IAstVisitor.VisitList(AstList ast)
        {
            if (ast.Length == 0)
            {
                return;
            }

            var first = (AstSymbol)ast[0];

            switch (first.Name)
            {
                case "def!":
                    DefineMethod(ast);
                    break;
                case "add":
                case "+":
                    VisitRestOfList(ast);
                    AddInstr(new AddInstruction());
                    break;
                case "sub":
                case "-":
                    VisitRestOfList(ast);
                    AddInstr(new SubtractionInstruction());
                    break;
                case "mul":
                case "*":
                    VisitRestOfList(ast);
                    AddInstr(new MultiplicationInstruction());
                    break;
                case "div":
                case "/":
                    VisitRestOfList(ast);
                    AddInstr(new DivisionInstruction());
                    break;
                case "quote":
                    GenerateQuote(ast);
                    break;
                default:
                    VisitRestOfList(ast);
                    AddInstr(new CallInstruction(first.Name));
                    break;
            }
        }

        private void GenerateQuote(AstList ast)
        {
            AddInstr(new ListNewInstruction());

            if (ast.Length != 2)
            {
                throw new InvalidOperationException("Unexpected length for quote function");
            }

            var datum = ast[1];

            switch (datum)
            {
                case AstList list:
                    for (var i = 0; i < list.Length; i++)
                    {
                        Visit(list[i]);
                        AddInstr(new ListAddInstruction());
                    }
                    break;
                default:
                    Visit(datum);
                    break;

            }
        }

        private void VisitRestOfList(AstList ast)
        {
            for (var i = 1; i < ast.Length; i++)
            {
                Visit(ast[i]);
            }
        }

        private void DefineMethod(AstList ast)
        {
            if (ast.Length < 3)
            {
                throw new InvalidOperationException("Too few list parameters");
            }

            var nameSymbol = ast[1];
            if (nameSymbol is AstSymbol s)
            {
                this.userDefinedFunctions[s.Name] = new VMFunction(s.Name, Eval(ast[2], new ReturnInstruction()));

            }
            else
            {
                throw new InvalidOperationException($"Expected to find symbol at index 1");
            }
        }

        void IAstVisitor.VisitNumber(AstNumber ast)
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

        void IAstVisitor.VisitSymbol(AstSymbol ast) =>
            AddInstr(new CallInstruction(ast.Name));

        void IAstVisitor.VisitString(AstString ast) =>
            AddInstr(new LoadStringInstruction(ast.Value));

        private IEnumerable<IInstruction> Eval(IAstNode node, IInstruction atTheEnd)
        {
            var evalVisitor = new CodeGenVisitor(this.readOnlyEnvData);
            node.Accept(evalVisitor);
            evalVisitor.instructions.Add(atTheEnd);
            return evalVisitor.instructions;
        }
    }
}
