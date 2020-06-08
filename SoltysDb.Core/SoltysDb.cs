using System;
using System.IO;
using System.Text;

namespace SoltysDb.Core
{
    public class SoltysDb : IDisposable
    {
        private DatabaseData data;
        public string FileName { get; }

        private const string InMemoryDatabaseId = ":memory:";
        public bool IsInMemoryDatabase => FileName == InMemoryDatabaseId;

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
                writer.Write(new HeaderPage());
            }
        }


        public void Insert(string key, string value)
        {
            var w = new DatabaseWriter(data);
            w.Write(new DataPage(Encoding.Default.GetBytes($"{key};{value}")));
        }

        public string Get(string key)
        {
            var pages = data.ReadAll();
            foreach (var page in pages)
            {
                var dataString = Encoding.Default.GetString(page.RawData).Trim('\0');
                var keyValuePair = dataString.Split(';');

                if (keyValuePair[0] == key)
                {
                    return keyValuePair[1];
                }
            }

            throw new DbKeyNotFoundException(key);
        }

        public void Dispose()
        {
            data?.Dispose();
        }
    }
}
