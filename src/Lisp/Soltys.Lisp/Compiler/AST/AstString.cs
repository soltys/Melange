using System;

namespace Soltys.Lisp.Compiler
{
    internal class AstString: IAstNode, IEquatable<AstString>
    {
        public string Value
        {
            get;
        }

        public AstString(string value)
        {
            Value = value;
        }

        public void Accept(IAstVisitor visitor) => visitor.VisitString(this);
        public override string ToString() => $"\"{Value}\"";
        public IAstNode Clone() => new AstString(Value);

        public bool Equals(AstString? other)
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

            return Equals((AstString) obj);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}
