namespace SoltysLib.Bson
{
    internal enum ElementType : byte
    {
        //http://bsonspec.org/spec.html
        // byte 	1 byte (8-bits)
        // int32 	4 bytes (32-bit signed integer, two's complement)
        // int64 	8 bytes (64-bit signed integer, two's complement)
        // uint64 	8 bytes (64-bit unsigned integer)
        // double 	8 bytes (64-bit IEEE 754-2008 binary floating point)
        // decimal128 	16 bytes (128-bit IEEE 754-2008 decimal floating point)


        Undefined = 0x00, //e_name double
        Double = 0x01,
        String = 0x02, // e_name string - UTF-8 string
        Document = 0x03, // Embedded document
        Array = 0x04,  // Array
        Binary = 0x05,
        ObjectId = 0x07,
        Boolean = 0x08, //e_name byte - where 0x00 - false or 0x01 - true
        DateTime = 0x09,
        NullValue = 0x0A,
        RegularExpression = 0x0B, //e_name cstring cstring 
        // Regular expression - The first cstring is the regex pattern, the second is the regex options string.
        // Options are identified by characters, which must be stored in alphabetical order.
        // Valid options are 'i' for case insensitive matching, 'm' for multiline matching, 'x' for verbose mode, 'l' to make \w, \W, etc. locale dependent, 's' for dotall mode ('.' matches everything), and 'u' to make \w, \W, etc. match unicode.

        Integer32 = 0x10,
        Timestamp = 0x11,
        Integer64 = 0x12,
        DecimalFloatingPoint = 0x13,

    }
}
