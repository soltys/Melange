using System;

namespace SoltysLib.Bson
{
    public static class BsonSerializer
    {
        public static BsonDocument Deserialize(ReadOnlySpan<byte> span)
        {
            var (doc, _) = BsonDecoder.DecodeBsonDocument(span);
            return doc;
        }

        public static byte[] Serialize(BsonDocument bsonDocument) => bsonDocument.GetBytes().ToArray();
    }
}
