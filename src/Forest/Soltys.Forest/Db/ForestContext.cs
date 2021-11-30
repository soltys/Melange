using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Soltys.Forest.Db
{
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


    public class DbKeyValue
    {
        public Guid Key
        {
            get;
            set;
        }

        public string? Value
        {
            get;
            set;
        }
    }

    public class DbKeyValueConfiguration : IEntityTypeConfiguration<DbKeyValue>
    {
        public void Configure(EntityTypeBuilder<DbKeyValue> builder)
        {
            builder.ToTable("KeyValue").HasKey(x => x.Key);
            builder.Property(x => x.Key).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();
        }
    }
}
