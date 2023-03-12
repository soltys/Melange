namespace Soltys.Library.Bson;

public class BsonString : BsonValue
{

    public override int GetHashCode() => (Value != null ? Value.GetHashCode() : 0);

    public string Value
    {
        get;
    }
    public BsonString(string value)
    {
        Value = value;
    }
    public override ReadOnlySpan<byte> GetBytes() => BsonEncoder.EncodeAsString(Value);

    public override string ToString() => $"\"{Value}\"";
    internal override ElementType Type => ElementType.String;

    public static bool operator ==(BsonString lhs, BsonString rhs) => lhs?.Equals((object)rhs) ?? ReferenceEquals(rhs, null);
    public static bool operator !=(BsonString lhs, BsonString rhs) => !(lhs == rhs);
        
    protected bool Equals(BsonString other) => Value == other.Value;

    public override bool Equals(object obj)
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

        return Equals((BsonString)obj);
    }
}
