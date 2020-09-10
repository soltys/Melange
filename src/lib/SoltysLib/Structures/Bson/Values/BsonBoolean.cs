using System;

namespace SoltysLib.Bson
{
    public class BsonBoolean : BsonValue
    {
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
    }
}
