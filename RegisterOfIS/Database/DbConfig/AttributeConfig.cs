using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RegisterOfIS.Database.DbConfig
{
    public class AttributeConfig : IEntityTypeConfiguration<Models.Attribute>
    {
        public void Configure(EntityTypeBuilder<Models.Attribute> e)
        {

            e.ToTable("attributes");

            e.HasMany(a => a.States)
                .WithOne(s => s.Attribute)
                .HasForeignKey(a => a.AttributeId);
        }
    }
}
