namespace SoltysDb.Core
{
    internal abstract class AstNode
    {
        public string Value { get; set; }
        public override string ToString() => $"{Value}";
    }
}