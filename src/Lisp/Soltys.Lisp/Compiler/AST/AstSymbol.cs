using System;

namespace Soltys.Lisp.Compiler
{
    internal class AstSymbol: IAstNode, IEquatable<AstSymbol>
    {
        public string Name
        {
            get;
        }
        public AstSymbol(string name)
        {
            Name = name;
        }

        public void Accept(IAstVisitor visitor) => visitor.VisitSymbol(this);
        public override string ToString() => Name;
        public IAstNode Clone() => new AstSymbol(Name);


        public bool Equals(AstSymbol? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name;
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

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((AstSymbol) obj);
        }

        public override int GetHashCode() => Name.GetHashCode();
    }
}
