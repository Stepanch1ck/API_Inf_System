using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegisterOfIS.Models;
using Microsoft.EntityFrameworkCore;

namespace RegisterOfIS.Database.DbConfig
{
    public class AttributeStateConfig : IEntityTypeConfiguration<AttributeState>
    {
        public void Configure(EntityTypeBuilder<AttributeState> e)
        {
            e.ToTable("attribute_states");

            e.HasKey(e => new { e.InfSystemId, e.AttributeId })
                .HasName("attribute_state_pkey");

            e.HasOne(e => e.InfSystem)
                .WithMany(infs => infs.AttributeStates)
                .HasForeignKey(e => e.InfSystemId)
                .HasConstraintName("attribute_state_inf_system_fkey");

            e.HasOne(e => e.Attribute)
                .WithMany(a => a.States)
                .HasForeignKey(e => e.AttributeId)
                .HasConstraintName("attribute_state_attribute_fkey");
        }
    }
}
