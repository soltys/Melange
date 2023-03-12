using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Soltys.Forest.Db;

public class DbKeyValueConfiguration : IEntityTypeConfiguration<DbKeyValue>
{
    public void Configure(EntityTypeBuilder<DbKeyValue> builder)
    {
        builder.ToTable("KeyValue").HasKey(x => x.Key);
        builder.Property(x => x.Key).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();
    }
}