using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Soltys.SoltysDb("file.db");
            
            for (int i = 0; i < 69; i++)
            {
                db.KV.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            }
        }
    }
}
