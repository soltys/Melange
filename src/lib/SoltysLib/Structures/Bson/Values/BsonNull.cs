using System;

namespace SoltysLib.Bson
{
    public class BsonNull : BsonValue
    {
        private readonly static BsonNull value = new BsonNull();
        public static BsonNull Value => BsonNull.value;
        private BsonNull()
        {

        }

        public override ReadOnlySpan<byte> GetBytes() => default;
        internal override ElementType Type => ElementType.NullValue;

        public override string ToString() => "null";
    }
}
