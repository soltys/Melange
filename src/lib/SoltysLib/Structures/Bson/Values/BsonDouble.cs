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
    }
}
