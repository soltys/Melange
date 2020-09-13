using System;
using System.Collections.Generic;
using System.Linq;

namespace SoltysLib.Bson
{
    public class BsonArray : BsonValue
    {
        internal override ElementType Type => ElementType.Array;
        private readonly List<BsonValue> values;
        public IReadOnlyCollection<BsonValue> Values => this.values;

        public BsonArray()
        {
            this.values = new List<BsonValue>();
        }

        public BsonArray(params BsonValue[] values)
        {
            this.values = new List<BsonValue>(values);
        }

        public void Add(BsonValue value)
        {
            this.values.Add(value);
        }

        public void AddRange(IEnumerable<BsonValue> v)
        {
            this.values.AddRange(v);
        }

        public BsonValue this[int index] => this.values[index];

        public override ReadOnlySpan<byte> GetBytes() => BsonEncoder.EncodeAsDocument(ToElements());

        /// <summary>
        /// Array - The document for an array is a normal BSON document with integer values for the keys,
        /// starting with 0 and continuing sequentially. For example, the array ['red', 'blue'] would be encoded as the document {'0': 'red', '1': 'blue'}.
        /// The keys must be in ascending numerical order.
        ///</summary>
        private IEnumerable<Element> ToElements() => this.values.Select((value, i) => new Element(i.ToString(), value));

        public override string ToString() => $"[{ToString(this.values)}]";

        private static string ToString(IEnumerable<BsonValue> values) => string.Join(", ", values.Select(x => x.ToString()));
    }
}
