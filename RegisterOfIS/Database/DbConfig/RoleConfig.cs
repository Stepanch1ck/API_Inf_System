using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegisterOfIS.Models;

namespace RegisterOfIS.Database.DbConfig
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> e)
        {
            e.ToTable("roles");

            e.HasKey(e => e.Id).HasName("role_pkey");

            e.Property(e => e.Id)
                .HasColumnName("id");

            e.Property(e => e.Name)
                .HasColumnName("name");

            e.Property(e => e.Description)
                .HasColumnName("description");

            e.HasData(
                new Role { Id = -1, Name = "ADMIN", Description = "full access"},
                new Role { Id = -2, Name = "CREATOR", Description = "create and read access"},
                new Role { Id = -3, Name = "READER", Description = "read access"}
                );
        }
    }
}
