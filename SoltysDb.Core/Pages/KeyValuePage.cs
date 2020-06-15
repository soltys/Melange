using System;
using System.Collections.Generic;

namespace SoltysDb.Core
{
    class KeyValuePage : DataPage
    {
        public Dictionary<string, string> GetReadStore()
            => KeyValueStoreSerializer.GetDictionaryFromBytes(DataBlock.Data);

        public void GetWriteStore(Action<Dictionary<string, string>> modify)
        {
            var dict = KeyValueStoreSerializer.GetDictionaryFromBytes(DataBlock.Data);

            modify(dict);

            KeyValueStoreSerializer.CovertDictionaryToBytes(dict).AsSpan().CopyTo(DataBlock.Data);
        }

        public KeyValuePage(Page page) : base(page)
        {
            page.PageType = PageType.KeyValue;
        }
    }
}
