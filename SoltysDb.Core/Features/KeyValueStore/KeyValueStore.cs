using System;
using System.Collections.Generic;
using System.Text;
using SoltysDb.Core.Pages;

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
            w.Write(new DataPage(
                new Page(Encoding.Default.GetBytes($"{key};{value}"))));new Page(Encoding.Default.GetBytes($"{key};{value}"));
        }

        public string Get(string key)
        {
            var pages = DatabaseData.ReadAll();
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

    }
}
