using System.Collections.Generic;

namespace SoltysDb.Core
{
    public interface IKeyValueStore
    {
        void Add(string key, string value);
        string Get(string key);
        bool Remove(string key);
        Dictionary<string, string> AsDictionary();

    }
}