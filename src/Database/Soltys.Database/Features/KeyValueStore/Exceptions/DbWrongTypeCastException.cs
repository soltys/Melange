using System;

namespace Soltys.Database
{
    internal class DbWrongTypeCastException : Exception
    {
        public string Key
        {
            get;
        }

        public Type KeyType
        {
            get;
        }

        public DbWrongTypeCastException(string key, Type keyType) : base($"Key {key} was wrongly casted, {key} is type of {keyType}")
        {
            Key = key;
            KeyType = keyType;
        }
    }
}
