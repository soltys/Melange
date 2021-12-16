using System.Linq;
using Soltys.Forest.Db;
using Soltys.Forest.Repository;
using TinyIoC;

namespace Soltys.Forest 
{
    
    public class Program
    {
        public static void Main(string[] args)
        {
            TinyIoCContainer container = new();
            container.Register<IDbRepository, DbRepository>().AsSingleton();

            var repo = container.Resolve<IDbRepository>();
            using var db = new ForestContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var book = new DbKeyValue()
            {
                Value = "foo"
            };
            db.Add(book);
            db.SaveChanges();
                

            var books = db.KeyValues!.ToList();
        }
    }
}
