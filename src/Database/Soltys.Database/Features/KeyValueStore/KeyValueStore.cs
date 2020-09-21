using System;
using System.Collections.Generic;
using Soltys.Library.Bson;

namespace Soltys.Database
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

        public void Add(string key, BsonValue value)
        {
            var kvPage = FindOrCreateKeyValuePage(this.collection);
            GetWriteStore(kvPage, (store) =>
            {
                store.Add(key, value);
            });

            this.DatabaseData.Write(kvPage);
        }

        public void Add(string key, string value) => Add(key, new BsonString(value));
        public void Add(string key, int value) => Add(key, new BsonInteger(value));

        public BsonValue Get(string key)
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

        public string GetString(string key) => GetAndCastTo<BsonString>(key).Value;
        public int GetInteger(string key) => GetAndCastTo<BsonInteger>(key).Value;

        private TBsonValue GetAndCastTo<TBsonValue>(string key) where TBsonValue : BsonValue
        {
            var entry = Get(key);
            if (!(entry is TBsonValue bsonValue))
            {
                throw new DbWrongTypeCastException(key, entry.GetType());
            }

            return bsonValue;
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

        public Dictionary<string, BsonValue> AsDictionary()
        {
            var kvPage = FindOrCreateKeyValuePage(this.collection);

            return GetReadStore(kvPage, this.DatabaseData);
        }

        public Dictionary<string, BsonValue> GetReadStore(Page firstDataPage, DatabaseData data)
            => BsonSerializer.Deserialize(data.ReadDataBlockBytes(firstDataPage)).ToDictionary();

        public void GetWriteStore(Page firstDataPage, Action<Dictionary<string, BsonValue>> modify)
        {
            var oldBytes = this.DatabaseData.ReadDataBlockBytes(firstDataPage);
            var dict = BsonSerializer.Deserialize(oldBytes).ToDictionary();

            modify(dict);

            var newDictBytes = BsonSerializer.Serialize(new BsonDocument(dict));

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

                    store[locationKey] = new BsonInteger(newPage.PageId);
                    pageId = newPage.PageId;
                }
                else
                {
                    var entry = store[locationKey] as BsonInteger;
                    if (entry != null)
                    {
                        pageId = entry.Value;
                    }
                    else
                    {
                        throw new InvalidOperationException("Unexpected value type");
                    }
                }
            });

            return this.DatabaseData.Read(pageId);
        }
    }
}
