using System;

namespace Soltys.Library.Bson
{
    public class BsonBoolean : BsonValue
    {
        public override int GetHashCode() => Value.GetHashCode();

        internal override ElementType Type => ElementType.Boolean;
        public bool Value
        {
            get;
        }
        public BsonBoolean(bool value)
        {
            Value = value;
        }
        public override ReadOnlySpan<byte> GetBytes() => BitConverter.GetBytes(Value);
        public override string ToString() => Value.ToString().ToLowerInvariant();

        public static bool operator ==(BsonBoolean lhs, BsonBoolean rhs) => lhs?.Equals((object)rhs) ?? object.ReferenceEquals(rhs, null);
        public static bool operator !=(BsonBoolean lhs, BsonBoolean rhs) => !(lhs == rhs);

        protected bool Equals(BsonBoolean other) => Value == other.Value;

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

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((BsonBoolean)obj);
        }
    }
}
