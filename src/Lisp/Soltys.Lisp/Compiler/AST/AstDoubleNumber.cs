using System.Globalization;

namespace Soltys.Lisp.Compiler
{
    internal class AstDoubleNumber : AstNumber
    {
        public double Value
        {
            get;
        }

        public AstDoubleNumber(double value)
        {
            Value = value;
        }
        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
    }
}
