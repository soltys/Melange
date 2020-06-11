namespace SoltysDb.Core.Features.KeyValueStore
{
    internal class KeyValueStore : Feature, IKeyValueStore
    {
        internal KeyValueStore(DatabaseData data) : base(data)
        {
        }

        public void Insert(string key, string value)
        {
            var kvPage = DatabaseData.FindFirst<KeyValuePage>() ?? new KeyValuePage(new Page());

            var w = new DatabaseWriter(DatabaseData);
            var store = kvPage.GetStore();
            
            store.Add(key, value);

            kvPage.SaveStore(store);
            w.Write(kvPage);
        }

        public string Get(string key)
        {
            var kvPage = DatabaseData.FindFirst<KeyValuePage>();
            if (kvPage != null)
            {
                var store = kvPage.GetStore();
                if (store.ContainsKey(key))
                {
                    return store[key];
                }
            }

            throw new DbKeyNotFoundException(key);
        }

    }
}
