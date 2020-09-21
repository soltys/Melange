using System;

namespace Soltys.Library.Bson
{
    public abstract class BsonValue
    {
        internal abstract ElementType Type
        {
            get;
        }

        public abstract ReadOnlySpan<byte> GetBytes();
    }
}
