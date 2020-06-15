using System;
using System.Collections.Generic;

namespace SoltysDb.Core
{
    class KeyValuePage : DataPage
    {
        public Dictionary<string, string> GetReadStore()
            => KeyValueStoreSerializer.GetDictionaryFromBytes(DataBlock.Data);

        public void GetWriteStore(DatabaseData data, Action<Dictionary<string, string>> modify)
        {
            var oldBytes =  data.ReadDataBlock(this);
            var dict = KeyValueStoreSerializer.GetDictionaryFromBytes(oldBytes);

            modify(dict);

            var newBytes = KeyValueStoreSerializer.CovertDictionaryToBytes(dict);

            int bytesToBeWritten = newBytes.Length;
            DataPage currentPage = this;
            while (bytesToBeWritten > 0)
            {
                int startIndex = newBytes.Length - bytesToBeWritten;
                int copyLength = Math.Min(bytesToBeWritten, currentPage.DataBlock.Data.Length);
                newBytes.AsSpan().Slice(startIndex, copyLength).CopyTo(currentPage.DataBlock.Data);

                data.Write(currentPage);

                bytesToBeWritten -= currentPage.DataBlock.Data.Length;

                if (bytesToBeWritten > 0)
                {
                    if (currentPage.DataBlock.NextPageLocation > 0)
                    {
                        currentPage = (DataPage) data.Read(currentPage.DataBlock.NextPageLocation);
                    }
                    else
                    {
                        var newKvPage = new KeyValuePage(new Page());
                        data.Write(newKvPage);

                        currentPage.DataBlock.NextPageLocation = newKvPage.Position;
                        data.Write(currentPage);

                        currentPage = newKvPage;
                    }
                }
            }
        }

        public KeyValuePage(Page page) : base(page)
        {
            page.PageType = PageType.KeyValue;
        }
    }
}
