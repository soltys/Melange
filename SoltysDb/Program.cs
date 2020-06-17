using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace SoltysDb
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Core.SoltysDb("file.db");
            
            for (int i = 0; i < 69; i++)
            {
                db.KV.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            }
        }
    }
}
