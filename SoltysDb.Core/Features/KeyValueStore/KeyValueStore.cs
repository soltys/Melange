namespace SoltysDb.Core.Features.KeyValueStore
{
    internal class KeyValueStore : Feature, IKeyValueStore
    {
        internal KeyValueStore(DatabaseData data) : base(data)
        {
        }

        public void Insert(string key, string value)
        {
            var kvPage = this.DatabaseData.FindFirst<KeyValuePage>() ?? new KeyValuePage(new Page());

            this.DatabaseData.ReadDataBlock(kvPage);

            kvPage.GetWriteStore((store) =>
            {
               store.Add(key, value);
            });

            this.DatabaseData.Write(kvPage);
        }

        public string Get(string key)
        {
            var kvPage = this.DatabaseData.FindFirst<KeyValuePage>();
            if (kvPage != null)
            {
                var store = kvPage.GetReadStore();
                if (store.ContainsKey(key))
                {
                    return store[key];
                }
            }

            throw new DbKeyNotFoundException(key);
        }

    }
}
