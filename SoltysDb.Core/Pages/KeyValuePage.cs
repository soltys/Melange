using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using SoltysDb.Core.Pages;

namespace SoltysDb.Core
{
    class KeyValuePage : DataPage
    {
        public Dictionary<string, string> KeyValuesStore
        {
            get => KeyValueStoreSerializer.GetDictionaryFromBytes(this.Data);
            set
            {
                var dictBytes = KeyValueStoreSerializer.CovertDictionaryToBytes(value);
                dictBytes.AsSpan().CopyTo(Data);
            }
        }

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
