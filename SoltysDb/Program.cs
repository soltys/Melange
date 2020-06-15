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

            db.KV.Insert("myKey0", "myValue11");
            db.KV.Insert("myKey2", "myValue22");
            db.KV.Insert("myKey3", "myValue33");

            for (int i = 0; i < 1000; i++)
            {
                db.KV.Insert(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            }
        }
    }
}
