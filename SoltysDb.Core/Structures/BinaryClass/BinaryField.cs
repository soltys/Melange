﻿using System;

namespace SoltysDb.Core
{
    internal abstract class BinaryField
    {
        protected Memory<byte> fieldSpan;

        protected BinaryField(byte[] memory, int offset, int fieldLength)
        {
            Offset = offset;
            FieldLength = fieldLength;
            this.fieldSpan = new Memory<byte>(memory, Offset, fieldLength);
        }
        public int FieldLength { get; }
        public int Offset { get; }
        public int FieldEnd => Offset + FieldLength;
    }
}