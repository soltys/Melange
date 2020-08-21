namespace SoltysDb.Core
{
    internal class AstNode
    {
        public string Value { get; set; }
        public override string ToString() => $"{Value}";
    }
}