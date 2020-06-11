using System;
using System.Collections.Generic;
using System.Text;

namespace SoltysDb.Core.Pages
{
    public enum PageType : byte
    {
        Undefined =0,
        Header,
        DataPage,
        KeyValue
    }

    internal static class PageTypeExtensions
    {
        internal static byte ToByte(this PageType type)
        {
            return (byte) type;
        }
    }
}
