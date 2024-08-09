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

            e.HasKey(e => e.Name).HasName("role_pkey");

            e.Property(e => e.Name)
                .HasColumnName("name");

            e.Property(e => e.Description)
                .HasColumnName("description");

            e.HasData(
                new Role { Name = "ADMIN", Description = "full access"},
                new Role { Name = "CREATOR", Description = "create and read access"},
                new Role { Name = "READER", Description = "read access"}
                );
        }
    }
}
