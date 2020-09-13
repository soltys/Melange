using System;

namespace SoltysLib.Bson
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
    }
}
