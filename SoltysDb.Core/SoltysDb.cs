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
        public bool IsInMemoryDatabase => FileName == SoltysDb.InMemoryDatabaseId;

        public IKeyValueStore KV { get; set; }

        public SoltysDb()
        {
            FileName = SoltysDb.InMemoryDatabaseId;
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
                this.data = new DatabaseData(new MemoryStream());
            }
            else
            {
                this.data = new DatabaseData(new FileStream(FileName, FileMode.OpenOrCreate));
            }

            if (this.data.IsNew())
            {
                var writer = new DatabaseWriter(this.data);
                writer.Write(new HeaderPage(new Page()));
            }

            KV = new KeyValueStore(this.data);
        }


     
        public void Dispose()
        {
            this.data?.Dispose();
        }
    }
}
