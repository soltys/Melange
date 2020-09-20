using System;
using System.Globalization;

namespace SoltysLib.Bson
{
    public class BsonDouble : BsonValue
    {
        public double Value
        {
            get;
        }

        public BsonDouble(double value)
        {
            Value = value;
        }

        public override ReadOnlySpan<byte> GetBytes() => BitConverter.GetBytes(Value);
        internal override ElementType Type => ElementType.Double;
        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);

        public static bool operator ==(BsonDouble lhs, BsonDouble rhs) =>
            lhs?.Equals((object)rhs) ?? ReferenceEquals(rhs, null);

        public static bool operator !=(BsonDouble lhs, BsonDouble rhs) => !(lhs == rhs);

        protected bool Equals(BsonDouble other) => Value == other.Value;

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

            return Equals((BsonDouble)obj);
        }

        public override int GetHashCode() => Value.GetHashCode();
    }
}
