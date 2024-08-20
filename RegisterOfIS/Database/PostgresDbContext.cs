using Microsoft.EntityFrameworkCore;
using RegisterOfIS.Models;
using Attribute = RegisterOfIS.Models.Attribute;

namespace RegisterOfIS.Database
{
    public class PostgresDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> Users => Set<User>();
        public DbSet<InfSystem> InfSystems => Set<InfSystem>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Attribute> Attributes => Set<Attribute>();
        public DbSet<AttributeDict> AttributeDict => Set<AttributeDict>();
        public DbSet<AttributeState> AttributeStates => Set<AttributeState>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

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

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
