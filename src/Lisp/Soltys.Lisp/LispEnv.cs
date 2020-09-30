using System.Collections.Generic;
using Soltys.Lisp.Compiler;
using Soltys.VirtualMachine;

namespace Soltys.Lisp
{
    internal class LispEnv : IAstVisitor
    {
        
        
        public LispEnv()
        {
            
        }


        public IEnumerable<IAstNode> Eval(IEnumerable<IAstNode> ast)
        {
            return ast;
        }

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
          //          DefineMethod(ast);
                    break;

            }
        }

        void IAstVisitor.VisitNumber(AstNumber ast)
        {
        }

        void IAstVisitor.VisitSymbol(AstSymbol ast)
        {
        }

        void IAstVisitor.VisitString(AstString ast)
        {
        }
    }
}
