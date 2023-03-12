using System.Text;

namespace Soltys.Library.Bson;

internal static class BsonEncoder
{
    /// <summary>
    /// output ::= int32 (byte*) "\x00"
    /// String - The int32 is the number bytes in the (byte*) + 1 (for the trailing '\x00').
    /// The (byte*) is zero or more UTF-8 encoded characters.
    /// </summary>
    internal static byte[] EncodeAsString(string value)
    {
        var valueBytes = Encoding.UTF8.GetBytes(value);
        var finalSize = sizeof(int) + valueBytes.Length + 1;
        var output = new byte[finalSize];

        Array.Copy(BitConverter.GetBytes(valueBytes.Length + 1), output, sizeof(int));
        Array.Copy(valueBytes, 0, output, sizeof(int), valueBytes.Length);
        return output;
    }


    /// <summary>
    /// output ::= (byte*) "\x00"
    /// Zero or more modified UTF-8 encoded characters followed by '\x00'. The (byte*) MUST NOT contain '\x00', hence it is not full UTF-8.
    /// </summary>
    internal static byte[] EncodeAsCString(string value)
    {
        var bytes = new List<byte>(value.Length + 1);
        bytes.AddRange(Encoding.UTF8.GetBytes(value));
        bytes.Add(0x00);
        return bytes.ToArray();
    }

    internal static byte[] EncodeAsDocument(IEnumerable<Element> elements) 
    {
        var documentContent = elements.SelectMany(x => x.GetBytes().ToArray()).ToArray();
        var finalSize = sizeof(int) + documentContent.Length + 1;
        var bytes = new byte[finalSize];
        Array.Copy(BitConverter.GetBytes(finalSize), bytes, sizeof(int));
        Array.Copy(documentContent, 0, bytes, sizeof(int), documentContent.Length);
        return bytes;
    }
}
