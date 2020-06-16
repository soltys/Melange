namespace SoltysDb.Core
{
    public interface IKeyValueStore
    {
        void Insert(string key, string value);
        string Get(string key);
    }
}