using System;
using System.Collections.Generic;

namespace SoltysDb
{
    internal class KeyValueStore : Feature, IKeyValueStore
    {
        private string collection;
        private const string DefaultCollectionName = "$global";
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
            IPage kvPage = FindOrCreateKeyValuePage(this.collection);

            GetWriteStore(kvPage, this.DatabaseData, (store) =>
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
                GetWriteStore(kvPage, this.DatabaseData, (store) =>
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

        public Dictionary<string, string> GetReadStore(IPage firstDataPage, DatabaseData data)
            => KeyValueStoreSerializer.ToDictionary(data.ReadDataBlockBytes(firstDataPage));

        public void GetWriteStore(IPage firstDataPage, DatabaseData data, Action<Dictionary<string, string>> modify)
        {
            var oldBytes = data.ReadDataBlockBytes(firstDataPage);
            var dict = KeyValueStoreSerializer.ToDictionary(oldBytes);

            modify(dict);

            var newDictBytes = KeyValueStoreSerializer.ToBytes(dict);

            data.SaveDataInPages(firstDataPage, newDictBytes);
        }

        private IPage FindOrCreateKeyValuePage(string collectionName)
        {
            var headerPage = this.DatabaseData.Read(0);
            var location = 0L;
            GetWriteStore(headerPage, this.DatabaseData, (store) =>
            {
                var locationKey = collectionName + "_location";
                if (!store.ContainsKey(locationKey))
                {
                    var newPage = new Page(PageType.KeyValue);
                    this.DatabaseData.Write(newPage);

                    store[locationKey] = newPage.Position.ToString();
                    location = newPage.Position;
                }
                else
                {
                    location = long.Parse(store[locationKey]);
                }
            });

            return this.DatabaseData.Read(location);
        }
    }
}
