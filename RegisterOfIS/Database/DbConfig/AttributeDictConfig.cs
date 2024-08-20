using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegisterOfIS.Models;

namespace RegisterOfIS.Database.DbConfig
{
    public class AttributeDictConfig : IEntityTypeConfiguration<AttributeDict>
    {
        public void Configure(EntityTypeBuilder<AttributeDict> e)
        {
            e.ToTable("attribute_dict");

            e.HasOne(e => e.Attribute)
                .WithMany(a => a.Dict)
                .HasForeignKey(e => e.AttributeId);
        }
    }
}
