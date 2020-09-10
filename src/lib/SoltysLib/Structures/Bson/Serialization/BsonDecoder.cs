using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoltysLib.Bson
{
    internal static class BsonDecoder
    {
        internal static (BsonDocument, int) DecodeBsonDocument(ReadOnlySpan<byte> span)
        {
            var document = new BsonDocument();
            var (elements, offset) = DecodeList(span);
            document.AddRange(elements);
            return (document, offset);
        }

        private static (List<Element>, int) DecodeList(ReadOnlySpan<byte> span)
        {
            var elements = new List<Element>();
            var documentSize = BitConverter.ToInt32(span.Slice(0, sizeof(int)));
            var offset = sizeof(int);

            while (span[offset] != 0x00 && offset < documentSize)
            {
                var (element, bytesRead) = DecodeElement(span.Slice(offset));
                elements.Add(element);
                offset += bytesRead;
            }

            return (elements, offset);
        }


        private static (Element, int) DecodeElement(ReadOnlySpan<byte> span)
        {
            var elementType = (ElementType)span[0];
            var (elementName, cStringBytesRead) = DecodeCString(span.Slice(1));
            var (bsonValue, bytesRead) = DecodeValue(span.Slice(sizeof(ElementType) + cStringBytesRead), elementType, elementName);

            var element = new Element(elementName, bsonValue);

            return (element, bytesRead + sizeof(ElementType) + cStringBytesRead);
        }

        private static (BsonValue, int) DecodeValue(ReadOnlySpan<byte> slice, ElementType elementType, string elementName)
        {
            switch (elementType)
            {
                case ElementType.Double:
                    return DecodeBsonDouble(slice);
                case ElementType.String:
                    return DecodeBsonString(slice);
                case ElementType.Document:
                    return DecodeBsonDocument(slice);
                case ElementType.Array:
                    return DecodeBsonArray(slice);
                case ElementType.Binary:
                    return DecodeBsonBinary(slice);
                case ElementType.ObjectId:
                    break;
                case ElementType.Boolean:
                    return DecodeBsonBoolean(slice);
                case ElementType.DateTime:
                    return DecodeBsonDatetime(slice);
                case ElementType.NullValue:
                    return (BsonNull.Value, 0);
                case ElementType.RegularExpression:
                    break;
                case ElementType.Integer32:
                    return DecodeBsonInteger32(slice);
                case ElementType.Timestamp:
                    break;
                case ElementType.Integer64:
                    return DecodeBsonInteger64(slice);
                case ElementType.DecimalFloatingPoint:
                    break;
            }
            throw new ArgumentOutOfRangeException(nameof(elementType), elementType, "Element type was not recognized");
        }

        private static (BsonValue, int) DecodeBsonBinary(in ReadOnlySpan<byte> slice)
        {
            var binarySize = BitConverter.ToInt32(slice);
            var binarySubType = (BinarySubType)slice[sizeof(int)];
            var readBytes = sizeof(int) + sizeof(byte) + binarySize;
            return (new BsonBinary(binarySubType, slice.Slice(sizeof(int) + 1, binarySize).ToArray()), readBytes);
        }

        private static (BsonValue, int) DecodeBsonDatetime(in ReadOnlySpan<byte> slice)
        {
            var value = BitConverter.ToInt64(slice);

            return (new BsonDatetime(value), sizeof(long));
        }

        private static (BsonValue, int) DecodeBsonInteger64(in ReadOnlySpan<byte> slice)
        {
            var value = BitConverter.ToInt64(slice);
            return (new BsonLongInteger(value), sizeof(long));
        }

        private static (BsonValue, int) DecodeBsonInteger32(in ReadOnlySpan<byte> slice)
        {
            var value = BitConverter.ToInt32(slice);
            return (new BsonInteger(value), sizeof(int));
        }

        private static (BsonValue, int) DecodeBsonArray(in ReadOnlySpan<byte> slice)
        {
            var (elements, offset) = DecodeList(slice);
            var array = new BsonArray();
            array.AddRange(elements.Select(x => x.Value));
            return (array, offset);
        }


        private static (string, int) DecodeString(in ReadOnlySpan<byte> span)
        {
            var lengthBytesRead = sizeof(int);
            var stringLength = BitConverter.ToInt32(span);
            var stringItself = Encoding.UTF8.GetString(span.Slice(lengthBytesRead, stringLength - 1)); //do not want trailing 0x00
            var totalBytesRead = lengthBytesRead + stringItself.Length + 1; //last part is for trailing 0x00

            return (stringItself, totalBytesRead);
        }

        private static (string, int) DecodeCString(in ReadOnlySpan<byte> span)
        {
            int i = 0;
            while (span[i++] != 0x00 && i < span.Length) ;

            var s = Encoding.UTF8.GetString(span.Slice(0, i - 1));

            return (s, i);
        }

        private static (BsonBoolean, int) DecodeBsonBoolean(in ReadOnlySpan<byte> slice)
        {
            var value = BitConverter.ToBoolean(slice);
            return (new BsonBoolean(value), sizeof(bool));
        }

        private static (BsonString, int) DecodeBsonString(in ReadOnlySpan<byte> slice)
        {
            var (stringItself, bytesRead) = DecodeString(slice);
            return (new BsonString(stringItself), bytesRead);
        }
        private static (BsonValue, int) DecodeBsonDouble(in ReadOnlySpan<byte> slice)
        {
            var value = BitConverter.ToDouble(slice);
            return (new BsonDouble(value), sizeof(double));
        }
    }
}

