using System;
using System.IO;
using SoltysDb.Core.Features.KeyValueStore;

namespace SoltysDb.Core
{
    public class SoltysDb : IDisposable
    {
        private DatabaseData data;
        public string FileName { get; }

        private const string InMemoryDatabaseId = ":memory:";
        public bool IsInMemoryDatabase => FileName == InMemoryDatabaseId;

        public IKeyValueStore KV { get; set; }

        public SoltysDb()
        {
            FileName = InMemoryDatabaseId;
            Initialize();
        }

        public SoltysDb(string fileName)
        {
            FileName = fileName;
            Initialize();
        }

        private void Initialize()
        {
            if (IsInMemoryDatabase)
            {
                data = new DatabaseData(new MemoryStream());
            }
            else
            {
                data = new DatabaseData(new FileStream(FileName, FileMode.OpenOrCreate));
            }

            if (data.IsNew())
            {
                var writer = new DatabaseWriter(data);
                writer.Write(new HeaderPage(new Page()));
            }

            KV = new KeyValueStore(data);
        }


     
        public void Dispose()
        {
            data?.Dispose();
        }
    }
}
