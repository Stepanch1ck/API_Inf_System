using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegisterOfIS.Models;

namespace RegisterOfIS.Database.DbConfig
{
    public class InfSystemConfig : IEntityTypeConfiguration<InfSystem>
    {
        public void Configure(EntityTypeBuilder<InfSystem> e)
        {

            e.ToTable("inf_systems");

            e.HasMany(inf => inf.AttributeStatements)
                .WithOne(st => st.InfSystem)
                .HasForeignKey(st => st.InfSystemId);

            e.HasData(
                new InfSystem { Id = -1, Name = "IS1", Description = "Desc1"},
                new InfSystem { Id = -2, Name = "IS2", Description = "Desc2" }
                );
        }
    }
}
