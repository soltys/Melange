using System.Collections.Generic;

namespace SoltysDb.Core
{
    public interface IKeyValueStore
    {
        string DefaultCollection { get; }
        void Add(string key, string value);
        string Get(string key);
        bool Remove(string key);
        Dictionary<string, string> AsDictionary();
        void ChangeCollection(string name);
    }
}