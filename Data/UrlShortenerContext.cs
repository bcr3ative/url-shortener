using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    // Class used for setting up the database context
    public class UrlShortenerContext : DbContext
    {
        public UrlShortenerContext(DbContextOptions<UrlShortenerContext> opt) : base(opt)
        {
        }

        // Setup unique indexes on model create
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasIndex(i => i.UserName)
                .IsUnique();

            modelBuilder.Entity<UrlMap>()
                .HasIndex(i => new {i.Url, i.AccountId})
                .IsUnique();
        }

        // Database set of accounts
        public DbSet<Account> Accounts { get; set; }
        // Database set of url maps
        public DbSet<UrlMap> UrlMaps { get; set; }
    }
}
