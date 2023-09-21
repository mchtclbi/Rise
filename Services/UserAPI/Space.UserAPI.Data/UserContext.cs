using Microsoft.EntityFrameworkCore;
using Space.UserAPI.Models.Entities;
using Space.UserAPI.Data.Entities.Configuration;

namespace Space.UserAPI.Data
{
    public class UserContext : DbContext
    {
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=./;Initial Catalog=SpaceUser;Integrated Security=True; TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}