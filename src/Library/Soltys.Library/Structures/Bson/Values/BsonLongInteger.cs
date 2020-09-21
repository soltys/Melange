using System;

namespace Soltys.Library.Bson
{
    public class BsonLongInteger : BsonValue
    {
        public long Value
        {
            get;
        }

        public BsonLongInteger(long value)
        {
            this.Value = value;
        }

        public override ReadOnlySpan<byte> GetBytes() => BitConverter.GetBytes(Value);
        internal override ElementType Type => ElementType.Integer64;
        public override string ToString() => Value.ToString();
    }
}
