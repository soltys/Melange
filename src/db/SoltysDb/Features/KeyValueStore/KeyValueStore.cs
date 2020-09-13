using System;
using System.Collections.Generic;

namespace SoltysDb
{
    internal class KeyValueStore : Feature, IKeyValueStore
    {
        private string collection;
        private const string DefaultCollectionName = "soltysdb_kv";
        public string DefaultCollection => KeyValueStore.DefaultCollectionName;

        internal KeyValueStore(DatabaseData data) : base(data)
        {
            this.collection = KeyValueStore.DefaultCollectionName;
        }

        public void ChangeCollection(string name)
        {
            this.collection = name;
        }

        public void Add(string key, string value)
        {
            var kvPage = FindOrCreateKeyValuePage(this.collection);
            GetWriteStore(kvPage, (store) =>
            {
                store.Add(key, value);
            });

            this.DatabaseData.Write(kvPage);
        }

        public string Get(string key)
        {
            var kvPage = FindOrCreateKeyValuePage(this.collection);
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
            var kvPage = FindOrCreateKeyValuePage(this.collection);
            var wasRemoved = false;
            if (kvPage != null)
            {
                GetWriteStore(kvPage, (store) =>
               {
                   wasRemoved = store.Remove(key);
               });
            }

            return wasRemoved;
        }

        public Dictionary<string, string> AsDictionary()
        {
            var kvPage = FindOrCreateKeyValuePage(this.collection);

            return GetReadStore(kvPage, this.DatabaseData);
        }

        public Dictionary<string, string> GetReadStore(Page firstDataPage, DatabaseData data)
            => KeyValueStoreSerializer.ToDictionary(data.ReadDataBlockBytes(firstDataPage));

        public void GetWriteStore(Page firstDataPage, Action<Dictionary<string, string>> modify)
        {
            var oldBytes = this.DatabaseData.ReadDataBlockBytes(firstDataPage);
            var dict = KeyValueStoreSerializer.ToDictionary(oldBytes);

            modify(dict);

            var newDictBytes = KeyValueStoreSerializer.ToBytes(dict);

            this.DatabaseData.SaveDataInPages(firstDataPage, newDictBytes);
        }

        private Page FindOrCreateKeyValuePage(string collectionName)
        {
            var headerPage = this.DatabaseData.Read(0);
            var pageId = 0;
            GetWriteStore(headerPage, (store) =>
            {
                var locationKey = collectionName + "_location";
                if (!store.ContainsKey(locationKey))
                {
                    var newPage = new Page(PageKind.KeyValue);
                    this.DatabaseData.Write(newPage);

                    store[locationKey] = newPage.PageId.ToString();
                    pageId = newPage.PageId;
                }
                else
                {
                    pageId = int.Parse(store[locationKey]);
                }
            });

            return this.DatabaseData.Read(pageId);
        }
    }
}
