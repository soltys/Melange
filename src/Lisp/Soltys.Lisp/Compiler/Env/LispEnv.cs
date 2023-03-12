using Soltys.VirtualMachine.Contracts;

namespace Soltys.Lisp.Compiler;

internal class LispEnv :  IAstVisitor
{
    private readonly LispEnvData data = new LispEnvData();

    public IAstNode Eval(IAstNode ast)
    {
        ast.Accept(this);
        return ast;
    }

    public void VisitLibrary(IVMLibrary library)
    {
        this.data.RegisterLibraryNames(library.Functions.Keys);
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
                DefineMethod(ast);
                break;

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
            this.data.Defines[s] = Eval(ast[2]);

        }
        else
        {
            throw new InvalidOperationException($"Expected to find symbol at index 1");
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

    public IReadOnlyEnvData Copy() => this.data.Clone();
}
