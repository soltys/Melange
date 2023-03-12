using System.Globalization;

namespace Soltys.Lisp.Compiler;

internal class AstDoubleNumber : AstNumber, IEquatable<AstDoubleNumber>
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
    public override IAstNode Clone() => new AstDoubleNumber(Value);

    public bool Equals(AstDoubleNumber? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Value.Equals(other.Value);
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

        return Equals((AstDoubleNumber) obj);
    }

    public override int GetHashCode() => Value.GetHashCode();
}
