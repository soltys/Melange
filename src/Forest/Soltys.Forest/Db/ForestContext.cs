using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Soltys.Forest.Db;

internal class ForestContext : DbContext
{
    private readonly string connectionString;

    public ForestContext()
    {
        SqliteConnectionStringBuilder sb =
            new()
            {
                DataSource = "forest.db",
                Mode = SqliteOpenMode.ReadWriteCreate
            };

        this.connectionString = sb.ToString();
    }

    public ForestContext(string connectionString)
    {
        this.connectionString = connectionString;
    }
    public DbSet<DbKeyValue>? KeyValues
    {
        get;
        set;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder
            .UseSqlite(this.connectionString)
            .LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ForestContext).Assembly);
    }
}
