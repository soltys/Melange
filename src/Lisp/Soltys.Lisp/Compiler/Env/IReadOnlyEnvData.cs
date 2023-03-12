namespace Soltys.Lisp.Compiler;

internal interface IReadOnlyEnvData
{
    IReadOnlyDictionary<AstSymbol, IAstNode> Defines
    {
        get;
    }

    IReadOnlyCollection<string> CoreFunctionNames
    {
        get;
    }
}
