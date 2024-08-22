using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using RegistryOfInformationSystems.Models;
using Attribute = RegistryOfInformationSystems.Models.Attribute;

namespace RegistryOfInformationSystems.Database
{
    public class PostgresDbContext : DbContext
    {

        public DbSet<User> Users => Set<User>();
        public DbSet<InfSystem> InfSystems => Set<InfSystem>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Attribute> Attributes => Set<Attribute>();
        public DbSet<AttributeStatement> AttributeStatements => Set<AttributeStatement>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server = localhost; port = 5433; database = db_auth; UserID = admin; Password = root");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasOne(u => u.Role)
                                       .WithMany(r => r.Users)
                                       .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Attribute>().HasMany(a => a.States)
                                            .WithOne(aS => aS.Attribute)
                                            .HasForeignKey(aS => aS.AttributeId);

            modelBuilder.Entity<InfSystem>().HasMany(iS => iS.AttributeStatements)
                                            .WithMany(aS => aS.InfSystems)
                                            .UsingEntity(t => { });

        }
    }
}
