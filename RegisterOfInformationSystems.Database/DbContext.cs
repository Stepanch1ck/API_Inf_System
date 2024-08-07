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
        public DbSet<Core.Models.Attribute> Attributes => Set<Core.Models.Attribute>();
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
            modelBuilder.Entity<InformationSystem>().HasMany(inf => inf.AttributeStatements).WithMany(st => st.InformationSystems).UsingEntity(e => { });
            modelBuilder.Entity<InformationSystem>().HasMany(inf => inf.Attributes).WithMany(attr => attr.InformationSystems).UsingEntity(e => { });
            modelBuilder.Entity<User>().HasOne(us => us.Role).WithMany(role => role.Users).HasForeignKey(u => u.RoleId);
            modelBuilder.Entity<Attribute>().HasMany(a => a.States).WithOne(s => s.Attribute).HasForeignKey(a => a.AttributeId);
            
        }
    }
}
