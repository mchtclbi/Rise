using Space.DataAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Space.DataAPI.Data.Entities.Configuration;

namespace Space.DataAPI.Data
{
    public  class DataContext : DbContext
    {
        public DbSet<PriceHistory> PriceHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=./;Initial Catalog=SpacePriceHistory;Integrated Security=True; TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PriceHistoryConfiguration());
        }
    }
}