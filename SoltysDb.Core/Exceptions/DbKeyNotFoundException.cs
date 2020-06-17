using System;

namespace SoltysDb.Core
{
    class DbKeyNotFoundException :Exception
    {
        public DbKeyNotFoundException(string keyNotFound) :base($"Key {keyNotFound} is not found")
        {
            
        }
    }
}
