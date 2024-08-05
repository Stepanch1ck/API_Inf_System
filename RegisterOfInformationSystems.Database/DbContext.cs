using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RegisterOfInformationSystems.Core.Models;

namespace RegisterOfInformationSystems.Database
{
    public class PostgresDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> Users => Set<User>();
        public DbSet<InformationSystem> InfSystems => Set<InformationSystem>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Core.Models.Attribute> Attributes => Set<Core.Models.Attribute>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
        }
        public PostgresDbContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
    }
}
