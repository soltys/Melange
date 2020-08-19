using System;

namespace SoltysDb.Core.Exceptions
{
    class DbKeyNotFoundException :Exception
    {
        public DbKeyNotFoundException(string keyNotFound) :base($"Key {keyNotFound} is not found")
        {
            
        }
    }
}
