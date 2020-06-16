using System;
using System.Collections.Generic;

namespace SoltysDb.Core
{
    internal class KeyValueStore : Feature, IKeyValueStore
    {
        internal KeyValueStore(DatabaseData data) : base(data)
        {
        }

        public void Insert(string key, string value)
        {
            IPage kvPage = this.DatabaseData.FindFirst(PageType.KeyValue) ?? new Page(PageType.KeyValue);

            GetWriteStore(kvPage, this.DatabaseData, (store) =>
            {
                store.Add(key, value);
            });

            this.DatabaseData.Write(kvPage);
        }

        public string Get(string key)
        {
            var kvPage = this.DatabaseData.FindFirst(PageType.KeyValue);
            if (kvPage != null)
            {
                var store = GetReadStore(kvPage, this.DatabaseData);
                if (store.ContainsKey(key))
                {
                    return store[key];
                }
            }

            throw new DbKeyNotFoundException(key);
        }


        public Dictionary<string, string> GetReadStore(IPage firstDataPage, DatabaseData data)
            => KeyValueStoreSerializer.GetDictionaryFromBytes(data.ReadDataBlock(firstDataPage));

        public void GetWriteStore(IPage firstDataPage, DatabaseData data, Action<Dictionary<string, string>> modify)
        {
            var oldBytes = data.ReadDataBlock(firstDataPage);
            var dict = KeyValueStoreSerializer.GetDictionaryFromBytes(oldBytes);

            modify(dict);

            var newBytes = KeyValueStoreSerializer.CovertDictionaryToBytes(dict);

            int bytesToBeWritten = newBytes.Length;
            IPage currentPage = firstDataPage;
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
                        currentPage = data.Read(currentPage.DataBlock.NextPageLocation);
                    }
                    else
                    {
                        var newKvPage = new Page(PageType.KeyValue);
                        data.Write(newKvPage);

                        currentPage.DataBlock.NextPageLocation = newKvPage.Position;
                        data.Write(currentPage);

                        currentPage = newKvPage;
                    }
                }
            }
        }

    }
}
