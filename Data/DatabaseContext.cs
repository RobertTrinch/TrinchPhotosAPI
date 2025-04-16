using Microsoft.EntityFrameworkCore;
using TrinchPhotosAPI.Database.Models;
using TrinchPhotosAPI.Data.Models;

namespace TrinchPhotosAPI.Data
{
    public class DatabaseContext : DbContext
    {

        public DbSet<Galleries> Galleries { get; set; }
        public DbSet<Creators> Creators { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DatabaseContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite($"Data Source=sqlite-database-path");
            optionsBuilder.UseNpgsql("postgresql-connstring");
        }
        public DbSet<TrinchPhotosAPI.Data.Models.Products> Products { get; set; } = default!;

    }
}
