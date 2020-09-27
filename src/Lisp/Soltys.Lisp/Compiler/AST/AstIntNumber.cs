namespace Soltys.Lisp.Compiler
{
    internal class AstIntNumber : AstNumber
    {
        public int Value
        {
            get;
        }

        public AstIntNumber(int value)
        {
            Value = value;
        }
        public override string ToString() => Value.ToString();
    }
}
