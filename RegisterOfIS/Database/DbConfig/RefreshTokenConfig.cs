using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RegisterOfIS.Models;

namespace RegisterOfIS.Database.DbConfig
{
    public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> e)
        {
            e.ToTable("refresh_tokens");

            e.HasKey(e => e.Id).HasName("refresh_token_pkey");

            e.Property(e => e.Id).HasColumnName("id");
        }
    }
}
