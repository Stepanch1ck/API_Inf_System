using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReactApp2.Server.Models;

namespace ReactApp2.Server.DataAccess
{
    public class InfSystemDbContext(IConfiguration configuration) : DbContext
    {
        private readonly IConfiguration _configuration = configuration;

        public DbSet<InfSystem> InfSystems => Set<InfSystem>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
        }
    }
}
