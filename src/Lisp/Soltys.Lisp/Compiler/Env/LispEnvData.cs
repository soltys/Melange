namespace Soltys.Lisp.Compiler;

internal class LispEnvData : IReadOnlyEnvData
{
    public Dictionary<AstSymbol, IAstNode> Defines
    {
        get;
    } = new Dictionary<AstSymbol, IAstNode>();
    IReadOnlyDictionary<AstSymbol, IAstNode> IReadOnlyEnvData.Defines => Defines;

    private List<string> coreFunctionNames = new List<string>();
    IReadOnlyCollection<string> IReadOnlyEnvData.CoreFunctionNames => this.coreFunctionNames;


    public LispEnvData Clone()
    {
        var clone = new LispEnvData();
        CloneDefines(clone.Defines);
        clone.coreFunctionNames = new List<string>(this.coreFunctionNames);
        return clone;
    }

    private void CloneDefines(Dictionary<AstSymbol, IAstNode> destination)
    {
        foreach (var node in Defines)
        {
            destination.Add((AstSymbol)node.Key.Clone(), node.Value.Clone());
        }
    }

    public void RegisterLibraryNames(IEnumerable<string> functionsNames)
    {
        this.coreFunctionNames.AddRange(functionsNames);
    }
}
