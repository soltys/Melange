using Soltys.Library.Bson;

namespace Soltys.Database;

public interface IKeyValueStore
{
    string DefaultCollection { get; }
    //
    //Inserting data
    //
    void Add(string key, BsonValue value);
    void Add(string key, string value);
    void Add(string key, int value);

    //
    //Getting data
    //

    BsonValue Get(string key);
    string GetString(string key);
    int GetInteger(string key);


    bool Remove(string key);
    Dictionary<string, BsonValue> AsDictionary();
    void ChangeCollection(string name);
}
