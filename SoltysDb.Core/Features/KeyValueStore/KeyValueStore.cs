using System;
using System.Collections.Generic;
using System.Text;

namespace SoltysDb.Core.Features.KeyValueStore
{
    internal class KeyValueStore : Feature, IKeyValueStore
    {
        internal KeyValueStore(DatabaseData data) : base(data)
        {
        }

        public void Insert(string key, string value)
        {
            var w = new DatabaseWriter(DatabaseData);
            var kvPage = new KeyValuePage(new Page());
            kvPage.KeyValuesStore = new Dictionary<string, string>
            {
                {key, value}
            };
            w.Write(kvPage);
        }

        public string Get(string key)
        {
            var pages = DatabaseData.ReadAll();
            foreach (var page in pages)
            {
                if (page is KeyValuePage kvPage)
                {
                    if (kvPage.KeyValuesStore.ContainsKey(key))
                    {
                        return kvPage.KeyValuesStore[key];
                    }
                }
            }

            throw new DbKeyNotFoundException(key);
        }

    }
}
