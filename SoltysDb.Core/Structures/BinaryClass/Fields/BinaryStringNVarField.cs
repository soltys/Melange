using System;
using System.Text;

namespace SoltysDb.Core
{
    class BinaryStringNVarField : BinaryField
    {
        private readonly int maxStringLength;
        private BinaryInt32Field currentStringLength;

        public BinaryStringNVarField(byte[] memory, int offset, int maxStringLength) : base(memory, offset, (maxStringLength * sizeof(char)) + sizeof(int))
        {
            this.maxStringLength = maxStringLength;
            this.currentStringLength = new BinaryInt32Field(memory, offset);
            this.fieldSpan = new Memory<byte>(memory, currentStringLength.FieldEnd, maxStringLength * sizeof(char));
            
        }

        public string ToValue()
        {
            var encodedString = Encoding.Default.GetString(fieldSpan.Span.ToArray());
            return encodedString.Substring(0, currentStringLength.ToValue());
        }

        public void SetValue(string value)
        {
            this.currentStringLength.SetValue(value.Length);
            var bytes = Encoding.Default.GetBytes(value);
            bytes.CopyTo(this.fieldSpan);
        }
    }
}
