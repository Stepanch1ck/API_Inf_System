using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegisterOfIS.Models;
using Microsoft.EntityFrameworkCore;

namespace RegisterOfIS.Database.DbConfig
{
    public class AttributeStatementConfig : IEntityTypeConfiguration<AttributeStatement>
    {
        public void Configure(EntityTypeBuilder<AttributeStatement> e)
        {

            e.ToTable("attribute_statements");
        }
    }
}
