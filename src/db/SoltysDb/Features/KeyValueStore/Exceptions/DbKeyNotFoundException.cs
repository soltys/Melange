using System;

namespace SoltysDb
{
    internal class DbKeyNotFoundException :Exception
    {
        public string KeyNotFound
        {
            get;
        }

        public DbKeyNotFoundException(string keyNotFound) :base($"Key {keyNotFound} is not found")
        {
            KeyNotFound = keyNotFound;
        }
    }
}
