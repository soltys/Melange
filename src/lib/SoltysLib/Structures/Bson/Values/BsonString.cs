using System;

namespace SoltysLib.Bson
{
    public class BsonString : BsonValue
    {
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
    }
}
