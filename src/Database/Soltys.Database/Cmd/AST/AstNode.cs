namespace Soltys.Database;

internal abstract class AstNode
{
    public string Value { get; set; }
    public override string ToString() => $"{Value}";
}