using System;

namespace Soltys.Library.Bson
{
    public class BsonInteger : BsonValue
    {
        public override int GetHashCode() => Value;

        public int Value
        {
            get;
        }

        public BsonInteger(int value)
        {
            Value = value;
        }

        public override ReadOnlySpan<byte> GetBytes() => BitConverter.GetBytes(Value);
        internal override ElementType Type => ElementType.Integer32;
        public override string ToString() => Value.ToString();

        public static bool operator ==(BsonInteger lhs, BsonInteger rhs) => lhs?.Equals((object)rhs) ?? object.ReferenceEquals(rhs, null);
        public static bool operator !=(BsonInteger lhs, BsonInteger rhs) => !(lhs == rhs);

        protected bool Equals(BsonInteger other) => Value == other.Value;

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

            return Equals((BsonInteger)obj);
        }
    }
}
