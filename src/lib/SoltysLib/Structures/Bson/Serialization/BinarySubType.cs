using System;

namespace SoltysLib.Bson
{
    public enum BinarySubType : byte
    {
        // |    "\x00" 	Generic binary subtype
        // | 	"\x01" 	Function
        // | 	"\x02" 	Binary (Old)
        // | 	"\x03" 	UUID (Old)
        // | 	"\x04" 	UUID
        // | 	"\x05" 	MD5
        // | 	"\x06" 	Encrypted BSON value
        // | 	"\x80" 	User defined

        /// <summary>  Binary data </summary>
        Binary = 0x00,
        Function = 0x01,
        [Obsolete("use Binary")]
        OldBinary = 0x02,
        UUIDLegacy = 0x03,
        UUID = 0x04,
        MD5 = 0x05,
        Encrypted = 0x06,
        UserDefined = 0x80,
    }
}
