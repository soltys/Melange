using System;
using System.Collections.Generic;

namespace SoltysDb.Core
{
    class KeyValuePage : DataPage
    {

        public Dictionary<string, string> GetStore()
            => KeyValueStoreSerializer.GetDictionaryFromBytes(this.Data);

        public void SaveStore(Dictionary<string, string> store)
            => KeyValueStoreSerializer.CovertDictionaryToBytes(store).AsSpan().CopyTo(Data);

        public KeyValuePage(Page page) : base(page)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            page.PageType = PageType.KeyValue;
        }
    }
}
