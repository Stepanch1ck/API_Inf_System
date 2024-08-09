using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegisterOfIS.Models;

namespace RegisterOfIS.Database.DbConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> e)
        {

            e.ToTable("users");

            e.HasOne(us => us.Role)
                .WithMany(role => role.Users)
                .HasForeignKey(u => u.RoleName)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("user_role_id_fkey");

            e.HasData(
                new User { Id = Guid.NewGuid(), Login = "admin", RoleName = "ADMIN", 
                    Password = "$2a$11$hmuUGLf.gBactT0CH3GWzOiQyRkZf3oDy2TNgYd1ttc2AL5rm.h.y" }, // password = admin
                new User { Id = Guid.NewGuid(), Login = "reader", RoleName = "READER", 
                    Password = "$2a$11$ibU9O5Nsfi0TeS7J7GHpqOCE6FapVU3Y5RtzAEOKKQph3CgDLnrJy" } // password = password
                );
        }
    }
}
