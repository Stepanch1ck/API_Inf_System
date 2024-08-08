using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RegisterOfInformationSystems.Core.Models;
using Attribute = RegisterOfInformationSystems.Core.Models.Attribute;

namespace RegisterOfInformationSystems.Database
{
    public class PostgresDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> Users => Set<User>();
        public DbSet<InformationSystem> InfSystems => Set<InformationSystem>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Attribute> Attributes => Set<Attribute>();
        public DbSet<AttributeStatement> AttributeStatements => Set<AttributeStatement>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
        }
        public PostgresDbContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(
                new Role(1, "admin", "Can do anything") { Id = -1},
                new Role(2, "Editor", "Can Edit Information Systems") { Id = -2 },
                new Role(3, "Reader", "Can Take Information") { Id = -3 }
                );
            modelBuilder.Entity<InformationSystem>().HasMany(inf => inf.AttributeStatements).WithMany(st => st.InformationSystems).UsingEntity("IStoAtStatements");
            modelBuilder.Entity<InformationSystem>().HasMany(inf => inf.Attributes).WithMany(attr => attr.InformationSystems).UsingEntity("IStoAtributes");
            modelBuilder.Entity<Role>().HasMany(r => r.Users).WithOne(us => us.Role).HasForeignKey(u => u.RoleId);
            modelBuilder.Entity<Attribute>().HasMany(a => a.States).WithOne(s => s.Attribute).HasForeignKey(a => a.AttributeId);
            
        }
    }
}
