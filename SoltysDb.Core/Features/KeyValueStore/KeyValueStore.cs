using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace SoltysDb.Core
{
    internal class KeyValueStore : Feature, IKeyValueStore
    {
        internal KeyValueStore(DatabaseData data) : base(data)
        {
        }

        public void Add(string key, string value)
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

        public bool Remove(string key)
        {
            var kvPage = this.DatabaseData.FindFirst(PageType.KeyValue);
            var wasRemoved = false;
            if (kvPage != null)
            {
                GetWriteStore(kvPage, this.DatabaseData, (store) =>
                {
                    wasRemoved = store.Remove(key);
                });
            }

            return wasRemoved;
        }

        public Dictionary<string, string> AsDictionary()
        {
            var kvPage = this.DatabaseData.FindFirst(PageType.KeyValue);
            if (kvPage != null)
            {
                return GetReadStore(kvPage, this.DatabaseData);
            }
            //Not KeyValueStore - for e.g. noting was inserted yet, returns empty dictionary
            return new Dictionary<string, string>();
        }

        public Dictionary<string, string> GetReadStore(IPage firstDataPage, DatabaseData data)
            => KeyValueStoreSerializer.GetDictionaryFromBytes(data.ReadDataBlockBytes(firstDataPage));

        public void GetWriteStore(IPage firstDataPage, DatabaseData data, Action<Dictionary<string, string>> modify)
        {
            var oldBytes = data.ReadDataBlockBytes(firstDataPage);
            var dict = KeyValueStoreSerializer.GetDictionaryFromBytes(oldBytes);

            modify(dict);

            var newDictBytes = KeyValueStoreSerializer.CovertDictionaryToBytes(dict);

            data.SaveDataInPages(firstDataPage, newDictBytes);
        }
    }
}
