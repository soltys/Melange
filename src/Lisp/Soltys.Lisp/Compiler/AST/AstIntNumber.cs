using System;

namespace Soltys.Lisp.Compiler
{
    internal class AstIntNumber : AstNumber, IEquatable<AstIntNumber>
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
        public override IAstNode Clone() => new AstIntNumber(Value);

        public bool Equals(AstIntNumber? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((AstIntNumber) obj);
        }

        public override int GetHashCode() => Value;
    }
}
