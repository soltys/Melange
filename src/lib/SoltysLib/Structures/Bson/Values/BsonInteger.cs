using System;

namespace SoltysLib.Bson
{
    public class BsonInteger : BsonValue
    {
        public int Value
        {
            get;
        }

        public BsonInteger(int value)
        {
            this.Value = value;
        }

        public override ReadOnlySpan<byte> GetBytes() => BitConverter.GetBytes(Value);
        internal override ElementType Type => ElementType.Integer32;
        public override string ToString() => Value.ToString();
    }
}
