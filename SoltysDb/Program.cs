namespace SoltysDb
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Core.SoltysDb("file.db");

            db.KV.Insert("myKey", "myValue");
        }
    }
}
