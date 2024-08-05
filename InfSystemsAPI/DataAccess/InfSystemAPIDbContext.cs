using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using InfSystemsAPI.Models;
using System.Collections.Generic;

namespace InfSystemsAPI.DataAccess
{
    public class InfSystemAPIDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> Users => Set<User>();
        public DbSet<InfSystem> InfSystems => Set<InfSystem>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Models.Attribute> Attributes => Set<Models.Attribute>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Database"));
        }
        public InfSystemAPIDbContext(IConfiguration configuration)
        {
            this._configuration = configuration;    
        }
    }
}
