using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace SoltysDb.Core
{
    internal class KeyValueStore : Feature, IKeyValueStore
    {
        private string collection;
        private const string DefaultCollectionName = "$global";
        public string DefaultCollection => DefaultCollectionName;

        internal KeyValueStore(DatabaseData data) : base(data)
        {
            this.collection = DefaultCollectionName;
        }

        public void ChangeCollection(string name)
        {
            this.collection = name;
        }

        public void Add(string key, string value)
        {
            IPage kvPage = FindKeyValuePage(this.collection);

            GetWriteStore(kvPage, this.DatabaseData, (store) =>
            {
                store.Add(key, value);
            });

            this.DatabaseData.Write(kvPage);
        }

        public string Get(string key)
        {
            var kvPage = FindKeyValuePage(this.collection);
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
            var kvPage = FindKeyValuePage(this.collection);
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
            var kvPage = FindKeyValuePage(this.collection);
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

        private IPage FindKeyValuePage(string name)
        {
            var headerPage = this.DatabaseData.Read(0);
            if (headerPage == null)
            {
                throw new InvalidOperationException("No header found");
            }
            var location = 0L;
            GetWriteStore(headerPage, this.DatabaseData, (store) =>
            {
                var locationKey = name + "_location";
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
