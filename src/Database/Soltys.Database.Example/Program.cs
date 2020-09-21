namespace Soltys.Database.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var db = new SoltysDb("file.db");

            //for (int i = 0; i < 69; i++)
            //{
            //    db.KV.Add(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            //}

            for (int i = 3000; i < 5000; i++)
            {
                db.KV.Add(i.ToString(), i.ToString());
            }
        }
    }
}
