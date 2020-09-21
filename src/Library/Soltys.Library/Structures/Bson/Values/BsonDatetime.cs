using System;

namespace Soltys.Library.Bson
{
    internal class BsonDatetime : BsonValue
    {
        

        private readonly long unixEpochMs;
        internal override ElementType Type => ElementType.DateTime;
        public DateTime Value
        {
            get;
        }

        public BsonDatetime(in long value)
        {
            this.unixEpochMs = value;
            Value = DateTimeOffset.FromUnixTimeMilliseconds(value).UtcDateTime;
        }

        public override ReadOnlySpan<byte> GetBytes() => BitConverter.GetBytes(this.unixEpochMs);

        public override string ToString() => $"Date({this.unixEpochMs})";

        public static bool operator ==(BsonDatetime lhs, BsonDatetime rhs) =>
            lhs?.Equals((object)rhs) ?? object.ReferenceEquals(rhs, null);

        public static bool operator !=(BsonDatetime lhs, BsonDatetime rhs) => !(lhs == rhs);

        protected bool Equals(BsonDatetime other) => Value == other.Value;

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

            return Equals((BsonDatetime)obj);
        }

        public override int GetHashCode() => this.unixEpochMs.GetHashCode();
    }
}
